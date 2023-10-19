namespace BokAdventure.Domain.Interfaces;

public interface IUserAccessor
{
    Guid Id { get; }
    bool IsAuthenticated { get; }
    string JwtToken { get; }
    IList<string> Roles { get; }
    string UserName { get; }
}