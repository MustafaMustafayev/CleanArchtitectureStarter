using Application.Settings;
using Infrastructure.DatabaseContexts;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, DatabaseSettings databaseSettings)
    {
        const string migrationAssemblyName = "Infrastructure";

        //Register the savechanges interceptor in the dependency container
        services.TryAddScoped<AuditingSaveChangesInterceptor>();

        //register database context
        services.AddDbContext<AppDbContext>((provider, dbContextOptionBuilder) =>
        {
            dbContextOptionBuilder.UseNpgsql(databaseSettings.ConnectionString, action =>
            {
                action.EnableRetryOnFailure(databaseSettings.MaxRetryCount);

                action.CommandTimeout(databaseSettings.CommandTimeout);

                action.MigrationsAssembly(migrationAssemblyName);
            });

            //add savechanges interceptor
            dbContextOptionBuilder.AddInterceptors(provider.GetRequiredService<AuditingSaveChangesInterceptor>());

            dbContextOptionBuilder.EnableDetailedErrors(databaseSettings.EnableDetailedErrors);
        });

        //register services
        services.Scan(scan => scan
                .FromAssemblies(typeof(AssemblyReference).Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(object)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        return services;
    }
}