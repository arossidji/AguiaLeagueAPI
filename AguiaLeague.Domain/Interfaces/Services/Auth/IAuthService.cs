using AguiaLeague.Domain.ValueObjects;

namespace AguiaLeague.Domain.Interfaces.Services.Auth;

public interface IAuthService : IDisposable
{
    Task<DiscordUserValueObject?> Autenticar(string code);
}