using Asp.Versioning.Builder;
using BokAdventure.Domain.Entities;
using BokAdventure.Domain.Interfaces;
using BokAdventure.Infrastructure.Authentication.Token;
using Carter;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BokAdventure.Api.UseCases.v1;

public sealed class Tokens : ICarterModule
{
    private readonly ApiVersionSet _apiVersionSet;

    public Tokens(ApiVersionSet apiVersionSet)
        => _apiVersionSet = apiVersionSet;
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var random = new Random();
        var version = random.Next(2, 234);

        var group = app.MapGroup("api/v{version:apiVersion}/Tokens")
            .WithTags(nameof(Tokens))
            .WithApiVersionSet(_apiVersionSet)
            .HasApiVersion(1)
            .HasApiVersion(version);

        group.MapGet("token-settings", (IOptions<TokenSettings> settings) => settings);
        group.MapGet("first-user-token", (ITokenService tokenService, UserManager<ApplicationUser> userManager) =>
        {
            return tokenService.GenerateToken(userManager.Users.Include(x => x.Player).FirstOrDefault() ?? throw new Exception("There no user in database"));
        });
        group.MapGet("check-authorization", () => Results.Ok())
            .RequireAuthorization();
    }
}
