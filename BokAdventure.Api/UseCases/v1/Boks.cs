﻿using BokAdventure.Domain.Entities;
using BokAdventure.Persistence;
using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Immutable;

namespace BokAdventure.Api.UseCases.v1;

public sealed class Boks : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .Build();

        var random = new Random();
        var version = random.Next(2, 234);

        var group = app.MapGroup("api/v{version:apiVersion}/Boks")
            .WithApiVersionSet(versionSet)
            .HasApiVersion(1)
            .HasApiVersion(version);

        group.MapGet("", Get);
    }
    public async Task<Ok<ImmutableList<Bok>>> Get(
        ApplicationDbContext appllicationDbContext)
    {
        return TypedResults.Ok(appllicationDbContext.Boks.ToImmutableList());
    }
}