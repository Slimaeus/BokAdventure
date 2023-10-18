namespace BokAdventure.Persistence;

using BokAdventure.Domain.Entities;
using BokAdventure.Domain.Enumerations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCore.AutoRegisterDi;
using Serilog;

[RegisterAsScoped]
public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationDbContextInitializer(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
    {
        _applicationDbContext = applicationDbContext;
        _userManager = userManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _applicationDbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Migration error");
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Seeding error");
        }
    }

    public async Task TrySeedAsync()
    {
        if (await _userManager.Users.AnyAsync()
            || await _applicationDbContext.Boks.AnyAsync()
            || await _applicationDbContext.Players.AnyAsync())
            return;

        var thai = new ApplicationUser
        {
            UserName = "thai",
            Email = "thai@gmail.com",
        };

        await _userManager.CreateAsync(thai, "P@ssw0rd");

        var thaiPlayer = new Player
        {
            ApplicationUserId = thai.Id,
            BokCoins = 1000000,
            ApplicationUser = thai,
        };

        await _applicationDbContext
            .AddAsync(thaiPlayer);

        var boks = new List<Bok>
        {
            new Bok
            {
                Id = BokIdentify.ASPNET,
                Name = nameof(BokIdentify.ASPNET),
                Type = BokType.Server,
                HitPoints = 0,
                Attack = 1,
                Defence = 0,
            },
            new Bok
            {
                Id = BokIdentify.PostgreSQL,
                Name = nameof(BokIdentify.PostgreSQL),
                Type = BokType.Database,
                HitPoints = 1,
                Attack = 0,
                Defence = 0,
            },
            new Bok
            {
                Id = BokIdentify.Flutter,
                Name = nameof(BokIdentify.Flutter),
                Type = BokType.Client,
                HitPoints = 0,
                Attack = 0,
                Defence = 1,
            },
            new Bok
            {
                Id = BokIdentify.MongoDB,
                Name = nameof(BokIdentify.MongoDB),
                Type = BokType.Client,
                HitPoints = 1,
                Attack = 0,
                Defence = 0,
            },
            new Bok
            {
                Id = BokIdentify.CSharp,
                Name = nameof(BokIdentify.CSharp),
                Type = BokType.Language,
                HitPoints = 0,
                Attack = 1,
                Defence = 0,
            },
            new Bok
            {
                Id = BokIdentify.Dart,
                Name = nameof(BokIdentify.Dart),
                Type = BokType.Language,
                HitPoints = 0,
                Attack = 0,
                Defence = 1,
            },
            new Bok
            {
                Id = BokIdentify.TypeScript,
                Name = nameof(BokIdentify.TypeScript),
                Type = BokType.Language,
                HitPoints = 0,
                Attack = 0,
                Defence = 1,
            }
        };

        await _applicationDbContext
            .AddRangeAsync(boks);

        await _applicationDbContext
            .SaveChangesAsync().ConfigureAwait(false);
    }
}