namespace BokAdventure.Persistence;
using Microsoft.EntityFrameworkCore;
using NetCore.AutoRegisterDi;
using Serilog;

[RegisterAsScoped]
public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ApplicationDbContextInitializer(ApplicationDbContext applicationDbContext)
        => _applicationDbContext = applicationDbContext;
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

    public Task TrySeedAsync()
    {
        return Task.CompletedTask;
    }
}