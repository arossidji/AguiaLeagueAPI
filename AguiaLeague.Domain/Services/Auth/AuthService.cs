using System.Net.Http.Headers;
using AguiaLeague.Domain.Interfaces.Services.Auth;
using AguiaLeague.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AguiaLeague.Domain.Services.Auth;

public class AuthService : IAuthService
{
    private const string DiscordApiBaseUrl = "https://discord.com/api";

    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<DiscordUserValueObject?> Autenticar(string code)
    {
        var accessToken = await ObterAccessToken(code);
        if (accessToken == null) return null;
        return await ObterUsuarioDiscord(accessToken);
    }

    private static async Task<DiscordUserValueObject?> ObterUsuarioDiscord(string accessToken)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var req = new HttpRequestMessage(HttpMethod.Get, $"{DiscordApiBaseUrl}/users/@me");
        var resposta = await client.SendAsync(req).ConfigureAwait(false);
        var content = JsonConvert.DeserializeObject<DiscordUserValueObject>(await resposta.Content.ReadAsStringAsync());
        return content;
    }

    private async Task<string?> ObterAccessToken(string code)
    {
        using var client = new HttpClient();
        var parametros = new List<KeyValuePair<string, string>>
        {
            new("client_id", _configuration["Discord:ClientId"]),
            new("client_secret", _configuration["Discord:ClientSecret"]),
            new("grant_type", "authorization_code"),
            new("code", code),
            new("redirect_uri", _configuration["Discord:RedirectUri"])
        };

        var req = new HttpRequestMessage(HttpMethod.Post, $"{DiscordApiBaseUrl}/v8/oauth2/token")
        {
            Content = new FormUrlEncodedContent(parametros)
        };
        var resposta = await client.SendAsync(req).ConfigureAwait(false);
        if (!resposta.IsSuccessStatusCode) return null;

        var content = JsonConvert.DeserializeObject<dynamic>(await resposta.Content.ReadAsStringAsync());
        return content?.access_token.ToString();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}