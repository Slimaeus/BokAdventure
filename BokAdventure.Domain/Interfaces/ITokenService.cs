using BokAdventure.Domain.Entities;

namespace BokAdventure.Domain.Interfaces;
public interface ITokenService
{
    string GenerateToken(ApplicationUser user);
}