using Asp.Versioning.Builder;
using BokAdventure.Application.Users.Commands;
using BokAdventure.Application.Users.Dtos;
using BokAdventure.Application.Users.Queries;
using BokAdventure.Domain.Common;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BokAdventure.Api.UseCases.v1;

public sealed class Users : ICarterModule
{
    private readonly ApiVersionSet _apiVersionSet;

    public Users(ApiVersionSet apiVersionSet)
        => _apiVersionSet = apiVersionSet;
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .Build();

        var random = new Random();
        var version = random.Next(2, 234);

        var group = app.MapGroup("api/v{version:apiVersion}/Players")
            .WithTags(nameof(Users))
            .WithApiVersionSet(_apiVersionSet)
            .HasApiVersion(1)
            .HasApiVersion(version);

        group.MapGet("@me", GetAccount);
        group.MapPost("login", Login);
    }

    public async Task<Ok<BokFlow<AccountDto>>> GetAccount(
        ISender sender,
        CancellationToken cancellationToken)
        => TypedResults.Ok(await sender.Send(new GetAccountQuery(), cancellationToken));

    public async Task<Ok<BokFlow<AccountDto>>> Login(
        ISender sender,
        LoginCommand request,
        CancellationToken cancellationToken)
        => TypedResults.Ok(await sender.Send(request, cancellationToken));
}
