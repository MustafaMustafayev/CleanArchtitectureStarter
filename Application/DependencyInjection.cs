using Application.Mappers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        const string excludeFolder = "Application.Responses";
        //register services
        services.Scan(scan => scan
                .FromAssemblies(typeof(AssemblyReference).Assembly)
                .AddClasses(classes => classes.NotInNamespaces(excludeFolder))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        //register fluent validations
        services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<AssemblyReference>();

        //register mediatrs
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

        //add auto mapper
        services.AddAutoMapper(MappingProfile.GetAutoMapperProfilesFromAllAssemblies().ToArray());

        return services;
    }
}