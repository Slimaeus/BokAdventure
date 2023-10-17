using BokAdventure.Domain.Constants;
using BokAdventure.Domain.Entities;
using BokAdventure.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BokAdventure.Infrastructure.Authentication.Token;

public sealed class TokenService : ITokenService
{
    private readonly SigningCredentials _tokenSigningCredentials;
    private readonly TimeSpan _tokenLifespan;

    public TokenService(IOptions<TokenSettings> settings)
    {
        var tokenKey = settings.Value.TokenKey;
        var tokenLifespan = settings.Value.TokenLifespan;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        _tokenSigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        _tokenLifespan = TimeSpan.FromHours(tokenLifespan);
    }

    public string GenerateToken(ApplicationUser user)
    {
        var tokenClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(ApplicationClaimTypes.PlayerIdentifier, user.PlayerId.ToString() ?? Guid.Empty.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(tokenClaims),
            Expires = DateTime.UtcNow.Add(_tokenLifespan),
            SigningCredentials = _tokenSigningCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
