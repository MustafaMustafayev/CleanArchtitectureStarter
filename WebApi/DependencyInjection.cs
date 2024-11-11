using StackExchange.Profiling.SqlFormatters;
using StackExchange.Profiling;
using WatchDog;
using WatchDog.src.Enums;
using Application.Settings;
using Microsoft.OpenApi.Models;

namespace WebApi;

public static class DependencyInjection
{
    public static void AddWatchDog(this IServiceCollection services, string databaseConnectionString)
    {
        services.AddWatchDogServices(options =>
        {
            options.IsAutoClear = true;
            options.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Weekly;
            options.DbDriverOption = WatchDogDbDriverEnum.PostgreSql;
            options.SetExternalDbConnString = databaseConnectionString;
        });
    }

    public static void AddMiniProfiler(this IServiceCollection services)
    {
        services.AddMiniProfiler(options =>
        {
            options.RouteBasePath = "/profiler";
            options.ColorScheme = ColorScheme.Dark;
            options.SqlFormatter = new InlineFormatter();
        }).AddEntityFramework();
    }

    public static void AddHealthCheck(this IServiceCollection services, string databaseConnectionString)
    {
        services.AddHealthChecks().AddNpgSql(databaseConnectionString);
    }

    public static void AddSwagger(this IServiceCollection services, ConfigSettings configSettings)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(configSettings.SwaggerSettings.Version,
                new OpenApiInfo { Title = configSettings.SwaggerSettings.Title, Version = configSettings.SwaggerSettings.Version });

            c.AddSecurityDefinition(configSettings.AuthSettings.TokenPrefix, new OpenApiSecurityScheme
            {
                Name = configSettings.AuthSettings.HeaderName,
                Type = SecuritySchemeType.ApiKey,
                Scheme = configSettings.AuthSettings.TokenPrefix,
                BearerFormat = configSettings.AuthSettings.Type,
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            c.AddSecurityDefinition(configSettings.AuthSettings.RefreshTokenHeaderName, new OpenApiSecurityScheme
            {
                Name = configSettings.AuthSettings.RefreshTokenHeaderName,
                In = ParameterLocation.Header,
                Description = "Refresh token header."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = configSettings.AuthSettings.TokenPrefix
                        }
                    },
                    Array.Empty<string>()
                },
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = configSettings.AuthSettings.RefreshTokenHeaderName
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}