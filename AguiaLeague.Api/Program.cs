using AguiaLeague.Data;
using AguiaLeague.Data.Repositories;
using AguiaLeague.Domain.Interfaces.Repositories;
using AguiaLeague.Domain.Interfaces.Services;
using AguiaLeague.Domain.Models;
using AguiaLeague.Domain.Services;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

#region Injeção de dependências
void InjetarDependencias(WebApplicationBuilder b)
{
    b.Services.AddScoped<ITimeService, TimeService>();
    b.Services.AddScoped<ITimeRepository, TimeRepository>();
}
#endregion