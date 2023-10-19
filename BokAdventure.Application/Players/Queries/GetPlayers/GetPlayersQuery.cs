using BokAdventure.Application.Common.Interfaces;
using BokAdventure.Application.Players.Dtos;
using BokAdventure.Domain.Common;
using BokAdventure.Domain.Entities;
using BokAdventure.Domain.Enumerations;
using BokAdventure.Domain.Helpers;
using BokAdventure.Domain.Interfaces;
using BokAdventure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BokAdventure.Application.Players.Queries.GetPlayers;

public sealed record GetPlayersQuery() : IQuery<BokFlow<IEnumerable<PlayerDto>>>;
public sealed class Handler : IQueryHandler<GetPlayersQuery, BokFlow<IEnumerable<PlayerDto>>>
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IBokFlowService _bokFlowService;

    public Handler(ApplicationDbContext applicationDbContext, IBokFlowService bokFlowService)
    {
        _applicationDbContext = applicationDbContext;
        _bokFlowService = bokFlowService;
    }
    public async Task<BokFlow<IEnumerable<PlayerDto>>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
    {
        var players = _applicationDbContext.Players
            .Include(x => x.PlayerBoks)
            .ThenInclude(x => x.Bok);

        var playerDtos = (await players
            .ToListAsync())
            .Select(x => new
            {
                Player = x,
                Hp = PowerCalculator.CalculateHitPoints(x.HitPoints, x.PlayerBoks
                    ?? Enumerable.Empty<PlayerBok>()),
                Atk = PowerCalculator.CalculateAttack(x.Attack, x.PlayerBoks
                    ?? Enumerable.Empty<PlayerBok>()),
                Def = PowerCalculator.CalculateDefence(x.Defence, x.PlayerBoks
                    ?? Enumerable.Empty<PlayerBok>())
            })
            .Select(x => new PlayerDto(
                x.Player.Id,
                x.Player.Level,
                x.Player.Experience,
                x.Player.BokCoins,
                x.Player.BokBank,
                x.Hp, x.Atk, x.Def))
            .ToList();

        var bokFlow = BokFlow<IEnumerable<PlayerDto>>.Create(playerDtos);
        var aspBok = await _bokFlowService.Get(BokIdentify.ASPNET);
        var postgreSQLBok = await _bokFlowService.Get(BokIdentify.PostgreSQL);
        bokFlow.AddBok(aspBok);
        bokFlow.AddBok(postgreSQLBok);

        return bokFlow;
    }
}
