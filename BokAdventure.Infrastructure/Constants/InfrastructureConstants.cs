using BokAdventure.Infrastructure.Authentication.Token;

namespace BokAdventure.Infrastructure.Constants;

public static class InfrastructureConstants
{
    public const string TokenSettingsSection = nameof(TokenSettings);
    public const string CookieUserToken = "user_token";
    public const string QueryAccessToken = "access_token";
}
