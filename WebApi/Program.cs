using Application.Settings;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Infrastructure;
using Domain;
using Application;
using WatchDog;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using WebApi.Helpers;
using Presentation;
using Serilog;
using WebApi.Middlewares;
using WebApi.ActionFilters;
using System.Text.Json.Serialization;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        //register serilog for error logging
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });

        //register config settings
        var configSettings = new ConfigSettings();
        builder.Configuration.GetSection(nameof(ConfigSettings)).Bind(configSettings);
        builder.Services.TryAddSingleton(configSettings);

        string databaseConnectionString = configSettings.DatabaseSettings.ConnectionString;

        //register httpcontextaccessor
        builder.Services.AddHttpContextAccessor();

        //register layers
        builder.Services.AddDomain()
                        .AddApplication()
                        .AddInfrastructure(configSettings.DatabaseSettings)
                        .AddPresentation(configSettings.AuthSettings.SecretKey)
                        .AddControllers(opt => opt.Filters.Add(typeof(ModelValidatorActionFilter)))
                        .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                        .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

        //register watchdog for request profiling
        builder.Services.AddWatchDog(databaseConnectionString);

        //register miniprofiler for ef core
        builder.Services.AddMiniProfiler();

        //register healt checks
        builder.Services.AddHealthCheck(databaseConnectionString);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        //register swagger
        if (configSettings.SwaggerSettings.IsEnabled)
        {
            builder.Services.AddSwagger(configSettings);
        }

        builder.Services.AddCors(o => o
                        .AddPolicy(Constants.CORS_POLICY_NAME, b => b
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin()));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (configSettings.SwaggerSettings.IsEnabled)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.EnablePersistAuthorization();
                c.InjectStylesheet(configSettings.SwaggerSettings.Theme);
            });
        }

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        app.UseCors(Constants.CORS_POLICY_NAME);

        app.UseMiddleware<LocalizationMiddleware>();
        app.UseMiddleware<ExceptionMiddleware>();

        /* add secutiry headers to response
        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            context.Response.Headers.Add("X-Frame-Options", "Deny");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");
            await next.Invoke();
        });
        */

        //use watchdog
        app.UseWatchDog(opt =>
        {
            opt.WatchPageUsername = "admin";
            opt.WatchPagePassword = "admin";
        });

        //use miniprofiler
        app.UseMiniProfiler();

        //use health check
        app.MapHealthChecks(
            "/health",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseAuthorization();
        app.UseAuthentication();

        app.MapControllers();

        app.Run();
    }
}