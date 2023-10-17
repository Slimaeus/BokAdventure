namespace BokAdventure.Infrastructure.Authentication.Token;

public sealed class TokenSettings
{
    public string TokenKey { get; set; } = string.Empty;
    public int TokenLifespan { get; set; }
}