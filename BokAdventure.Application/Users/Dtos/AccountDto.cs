namespace BokAdventure.Application.Users.Dtos;

public sealed record AccountDto(Guid Id, string UserName, string Email)
{
    public string? Token { get; set; }
}
