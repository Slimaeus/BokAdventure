namespace BokAdventure.Api.Configurations;

public static partial class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistenceServices(configuration);
        services.AddInfrastructureServices(configuration);
        services.AddApplicationServices(configuration);
        services.AddPresentationServices(configuration);
        return services;
    }

    public static async Task<WebApplication> UseServicesAsync(this WebApplication app)
    {
        await app.UsePersistenceServicesAsync();
        app.UsePresentationServices();

        return app;
    }
}
