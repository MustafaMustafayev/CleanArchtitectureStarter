
using Microsoft.Extensions.DependencyInjection;

namespace Domain;
public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        //////register repositories
        //services.Scan(scan => scan
        //        .FromAssemblies(typeof(AssemblyReference).Assembly)
        //        .AddClasses(classes => classes.AssignableTo(typeof(object)))
        //        .AsImplementedInterfaces()
        //        .WithScopedLifetime());

        return services;
    }
}
