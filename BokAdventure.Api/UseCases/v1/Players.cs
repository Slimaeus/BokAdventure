using BokAdventure.Application.Players.Dtos;
using BokAdventure.Domain.Entities;
using BokAdventure.Persistence;
using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Collections.Immutable;

namespace BokAdventure.Api.UseCases.v1;

public class Players : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/Players");

        group.MapGet("", Get);
        group.MapPost("register", Register);
    }
    private Task<Ok<ImmutableList<Player>>> Get(
        ApplicationDbContext applicationDbContext)
    {
        return Task.FromResult(TypedResults.Ok(applicationDbContext.Players.ToImmutableList()));
    }

    private async Task<Results<Ok<Guid>, BadRequest>> Register(
        ApplicationDbContext applicationDbContext,
        UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken,
        RegisterPlayerDto request)
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
            .AddAsync(player);

        var saveSuccess = await applicationDbContext
            .SaveChangesAsync(cancellationToken) > 0;

        if (!saveSuccess)
        {
            return TypedResults.BadRequest();
        }

        return TypedResults.Ok(player.Id);
    }
}
