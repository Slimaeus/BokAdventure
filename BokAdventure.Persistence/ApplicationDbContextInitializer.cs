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
                Name = "ASP.NET",
                Type = BokType.Server,
                HitPoints = 0,
                Attack = 1,
                Defence = 0,
            },
            new Bok
            {
                Name = "PostgreSQL",
                Type = BokType.Database,
                HitPoints = 1,
                Attack = 0,
                Defence = 0,
            },
            new Bok
            {
                Name = "Flutter",
                Type = BokType.Client,
                HitPoints = 0,
                Attack = 0,
                Defence = 1,
            },
            new Bok
            {
                Name = "MongoDB",
                Type = BokType.Client,
                HitPoints = 1,
                Attack = 0,
                Defence = 0,
            },
            new Bok
            {
                Name = "C#",
                Type = BokType.Language,
                HitPoints = 0,
                Attack = 1,
                Defence = 0,
            },
            new Bok
            {
                Name = "Dart",
                Type = BokType.Language,
                HitPoints = 0,
                Attack = 0,
                Defence = 1,
            },
            new Bok
            {
                Name = "TypeScript",
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