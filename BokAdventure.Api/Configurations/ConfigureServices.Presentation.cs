using Carter;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;
using NetCore.AutoRegisterDi;
using System.Text;
using System.Text.Json;

namespace BokAdventure.Api.Configurations;

public static partial class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            options.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

            options.AddSecurityRequirement(securityRequirement);

            options.CustomSchemaIds(type => type.FullName!.Replace("+", "."));
        });

        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        services.AddCarter();

        services.RegisterAssemblyPublicNonGenericClasses(
            Persistence.AssemblyReference.Assembly,
            Application.AssemblyReference.Assembly,
            Infrastructure.AssemblyReference.Assembly);

        return services;
    }
    public static WebApplication UsePresentationServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
            options.InjectStylesheet("/swagger/custom.css");
            options.EnableTryItOutByDefault();
        });

        const string CustomStyles = @"
            .swagger-ui .opblock .opblock-summary .view-line-link {
                margin: 0 5px;
                width: 24px;
            }";

        app.MapGet("/swagger/custom.css", () => Results.Text(CustomStyles, "text/css", Encoding.UTF8)).ExcludeFromDescription();

        app.MapCarter();

        app.UseHttpsRedirection();

        return app;
    }
}
