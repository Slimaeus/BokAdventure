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

        var player = new Player
        {
            ApplicationUserId = thai.Id,
            BokCoins = 1000000
        };

        await _applicationDbContext
            .AddAsync(player);

        var boks = new List<Bok>
        {
            new Bok
            {
                Type = BokType.AspNetCore
            },
            new Bok
            {
                Type = BokType.React
            },
            new Bok
            {
                Type = BokType.Flutter
            },
            new Bok
            {
                Type = BokType.PostgreSQL
            },
            new Bok
            {
                Type = BokType.MongoDB
            },
            new Bok
            {
                Type = BokType.Firebase
            },
            new Bok
            {
                Type = BokType.Redis
            },
            new Bok
            {
                Type = BokType.RabbitMQ
            }
        };

        await _applicationDbContext
            .AddRangeAsync(boks);

        await _applicationDbContext
            .SaveChangesAsync().ConfigureAwait(false);
    }
}