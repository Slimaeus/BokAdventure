using BokAdventure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BokAdventure.Api.Configurations;

public static partial class ConfigureServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTriggeredDbContextPool<ApplicationDbContext>(options =>
        {
            options.UseSqlite("DataSource=BokAdventure.db");
            options.UseTriggers(config => config.AddAssemblyTriggers(AssemblyReference.Assembly));
        });
        services.AddScoped<ApplicationDbContextInitializer>();
        return services;
    }

    public static async Task<WebApplication> UsePersistenceServicesAsync(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            using var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
            await initializer.InitialiseAsync();
            await initializer.SeedAsync();
        }
        return app;
    }
}
