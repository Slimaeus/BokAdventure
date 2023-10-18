namespace BokAdventure.Application.Players.Dtos;

public sealed record PlayerDto(
    Guid Id,
    ulong Level,
    ulong Experience,
    long BokCoins,
    long BokBank,
    ulong HitPoints,
    long Defence,
    long Attack);
