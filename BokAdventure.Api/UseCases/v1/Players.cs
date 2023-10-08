using BokAdventure.Domain.Entities;
using BokAdventure.Persistence;
using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Immutable;

namespace BokAdventure.Api.UseCases.v1;

public class Players : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/Players");

        group.MapGet("", Get);
    }
    private Task<Ok<ImmutableList<Player>>> Get(
        ApplicationDbContext applicationDbContext)
    {
        return Task.FromResult(TypedResults.Ok(applicationDbContext.Players.ToImmutableList()));
    }
}
