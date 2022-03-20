using AguiaLeague.Data;
using AguiaLeague.Data.Repositories;
using AguiaLeague.Domain.Interfaces.Repositories;
using AguiaLeague.Domain.Interfaces.Services;
using AguiaLeague.Domain.Interfaces.Services.Auth;
using AguiaLeague.Domain.Models;
using AguiaLeague.Domain.Services;
using AguiaLeague.Domain.Services.Auth;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;

IdentityModelEventSource.ShowPII = true;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(x => 
        x.RegisterValidatorsFromAssemblyContaining<Entity>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// PostgreSQL
builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<AguiaLeagueContext>(options =>
    {
        options.UseNpgsql(builder.Configuration["dbContextSettings:ConnectionString"]);
    });

// Injeção de dependências
InjetarDependencias(builder);

builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddHttpClient();

var app = builder.Build();
app.UseCors("default");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); //dotnet dev-certs https --trust
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

#region Injeção de dependências
void InjetarDependencias(WebApplicationBuilder b)
{
    b.Services.AddScoped<ITimeService, TimeService>();
    b.Services.AddScoped<ITimeRepository, TimeRepository>();
    b.Services.AddScoped<IAuthService, AuthService>();
}
#endregion