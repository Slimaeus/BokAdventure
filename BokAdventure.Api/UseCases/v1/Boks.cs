using Asp.Versioning.Builder;
using BokAdventure.Domain.Entities;
using BokAdventure.Domain.Enumerations;
using BokAdventure.Persistence;
using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace BokAdventure.Api.UseCases.v1;

public sealed class Boks : ICarterModule
{
    private readonly ApiVersionSet _apiVersionSet;

    public Boks(ApiVersionSet apiVersionSet)
        => _apiVersionSet = apiVersionSet;
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .Build();

        var random = new Random();
        var version = random.Next(2, 234);

        var group = app.MapGroup("api/v{version:apiVersion}/Boks")
            .WithTags(nameof(Boks))
            .WithApiVersionSet(_apiVersionSet)
            .HasApiVersion(1)
            .HasApiVersion(version);

        group.MapGet("", Get);
        group.MapGet("{id}", GetById);
    }
    public Ok<ImmutableList<Bok>> Get(
        ApplicationDbContext appllicationDbContext,
        [FromQuery] BokType type)
        => TypedResults.Ok(appllicationDbContext.Boks
            .Where(x => type == BokType.None || x.Type == type).ToImmutableList());

    public async Task<Ok<Bok>> GetById(
        ApplicationDbContext appllicationDbContext,
        [FromRoute] BokIdentify id)
        => TypedResults.Ok(await appllicationDbContext.Boks.FindAsync(id));
}
