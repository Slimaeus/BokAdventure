using Asp.Versioning.Builder;
using BokAdventure.Application.Players.Dtos;
using BokAdventure.Application.Players.Queries.GetPlayers;
using BokAdventure.Domain.Common;
using BokAdventure.Domain.Entities;
using BokAdventure.Domain.Enumerations;
using BokAdventure.Persistence;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BokAdventure.Api.UseCases.v1;

public sealed class Players : ICarterModule
{
    private readonly ApiVersionSet _apiVersionSet;

    public Players(ApiVersionSet apiVersionSet)
        => _apiVersionSet = apiVersionSet;
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        var versionSet = app.NewApiVersionSet()
            .Build();

        var random = new Random();
        var version = random.Next(2, 234);

        var group = app.MapGroup("api/v{version:apiVersion}/Players")
            .WithTags(nameof(Players))
            .WithApiVersionSet(_apiVersionSet)
            .HasApiVersion(1)
            .HasApiVersion(version);


        group.MapGet("", Get);
        group.MapPost("{id:guid}/add-bok/{bokId}/{amount}", AddBok);
        group.MapPost("register", Register);
        group.MapPost("add-exp/{id:guid}", AddExp);
    }

    private async Task<NoContent> AddExp(
        ApplicationDbContext applicationDbContext,
        Guid id,
        ulong amount,
        CancellationToken cancellationToken)
    {
        var player = await applicationDbContext.Players
            .FindAsync(new object?[] { id }, cancellationToken: cancellationToken)
            ?? throw new Exception("Player not found");

        player.Experience += amount;

        await applicationDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return TypedResults.NoContent();
    }

    private async Task<NoContent> AddBok(
        ApplicationDbContext applicationDbContext,
        Guid id,
        BokIdentify bokId,
        ulong amount,
        CancellationToken cancellationToken
        )
    {
        var player = await applicationDbContext.Players
            .Include(x => x.PlayerBoks)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new Exception("Player not found");

        var bok = await applicationDbContext.Boks
            .FindAsync(new object?[] { bokId }, cancellationToken: cancellationToken)
            ?? throw new Exception("Bok not found");

        var playerBok = player.PlayerBoks.SingleOrDefault(x => x.BokId == bokId);
        if (playerBok is null)
        {
            player.PlayerBoks.Add(new PlayerBok
            {
                PlayerId = id,
                BokId = bokId,
                Amount = amount
            });
        }
        else
        {
            playerBok.Amount += amount;
        }

        await applicationDbContext
            .SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        var bokFlow = BokFlow<object>.NoDataCreate();
        var bokInFlow = await applicationDbContext.Boks.SingleOrDefaultAsync(x => x.Id == BokIdentify.ASPNET, cancellationToken)
            ?? throw new Exception();
        bokFlow.AddBok(bokInFlow);

        return TypedResults.NoContent();
    }
    private async Task<Ok<BokFlow<IEnumerable<PlayerDto>>>> Get(
        ISender sender,
        CancellationToken cancellationToken) => TypedResults.Ok(await sender.Send(new GetPlayersQuery(), cancellationToken));

    private async Task<Results<Ok<Guid>, BadRequest>> Register(
        ApplicationDbContext applicationDbContext,
        UserManager<ApplicationUser> userManager,
        RegisterPlayerDto request,
        CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return TypedResults.BadRequest();
        }

        var player = new Player
        {
            ApplicationUser = user
        };

        await applicationDbContext.Players
            .AddAsync(player, cancellationToken);

        var saveSuccess = await applicationDbContext
            .SaveChangesAsync(cancellationToken) > 0;

        if (!saveSuccess)
        {
            return TypedResults.BadRequest();
        }

        return TypedResults.Ok(player.Id);
    }
}
