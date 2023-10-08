namespace BokAdventure.Api.Configurations;

public static partial class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddMediatR(options =>
        //{
        //    options.RegisterServicesFromAssemblies(Command.Application.AssemblyReference.Assembly);
        //});
        //services.AddAutoMapper(Command.Application.AssemblyReference.Assembly);
        //services.AddValidatorsFromAssembly(Command.Application.AssemblyReference.Assembly);
        return services;
    }
}
