using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using AguiaLeague.Data;
using AguiaLeague.Data.Repositories;
using AguiaLeague.Domain.Interfaces.Repositories;
using AguiaLeague.Domain.Interfaces.Services;
using AguiaLeague.Domain.Models;
using AguiaLeague.Domain.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

#region Discord OAuth2
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer  = true,
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:EncryptionKey"]))
        };
    })
    .AddOAuth("Discord", options =>
    {
        options.AuthorizationEndpoint = "https://discord.com/api/oauth2/authorize";
        options.Scope.Add("email");
        options.Scope.Add("guilds");
        options.CallbackPath = new PathString("/Login/Sucesso");
        options.ClientId = builder.Configuration["Discord:ClientId"];
        options.ClientSecret = builder.Configuration["Discord:ClientSecret"];
        options.TokenEndpoint = "https://discord.com/api/oauth2/token";
        options.UserInformationEndpoint = "https://discord.com/api/users/@me";
        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "username");
        options.AccessDeniedPath = "/Login/Erro";
        options.Events = new OAuthEvents
        {
            OnCreatingTicket = async context =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseContentRead, context.HttpContext.RequestAborted);
                response.EnsureSuccessStatusCode();

                var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
                context.RunClaimActions(user);
            }
        };
    });
#endregion

// Injeção de dependências
InjetarDependencias(builder);

var app = builder.Build();

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
}
#endregion