namespace AguiaLeague.Domain.ValueObjects;

public class DiscordUserValueObject
{
    public long Id { get; set; }
    public string Username { get; set; } = null!;
    public short Discriminator { get; set; }
    public string Avatar { get; set; } = null!;
    public bool Verified { get; set; }
    public string Email { get; set; } = null!;
}