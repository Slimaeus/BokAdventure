namespace BokAdventure.Application.Players.Dtos;

public record RegisterPlayerDto(string UserName, string Email, string Password, string ConfirmPassword);