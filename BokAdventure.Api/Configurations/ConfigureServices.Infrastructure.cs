﻿using BokAdventure.Domain.Entities;
using BokAdventure.Domain.Interfaces;
using BokAdventure.Infrastructure.Authentication.Token;
using BokAdventure.Infrastructure.Authentication.User;
using BokAdventure.Infrastructure.Boks;
using BokAdventure.Infrastructure.Constants;
using BokAdventure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace BokAdventure.Api.Configurations;

public static partial class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
                options.ClaimsIdentity.EmailClaimType = ClaimTypes.Email;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<TokenSettings>(configuration.GetSection(InfrastructureConstants.TokenSettingsSection));

        var tokenKey = services.BuildServiceProvider().GetRequiredService<IOptions<TokenSettings>>().Value.TokenKey;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var hasToken = context.Request.Cookies.TryGetValue(InfrastructureConstants.CookieUserToken, out var token);
                        if (hasToken && !string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }
                        var canGetAccessToken = context.Request.Query.TryGetValue("access_token", out var accessToken);
                        if (canGetAccessToken)
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(5)
                };
            });

        services.AddHttpContextAccessor();

        services.AddTransient<IUserAccessor, UserAccessor>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IBokFlowService, BokFlowService>();

        return services;
    }
}
