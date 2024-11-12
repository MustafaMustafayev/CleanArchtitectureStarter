using Application.Dtos.ErrorLog;
using Application.Interfaces;
using Application.Localization;
using Application.Responses;
using Application.Settings;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text.Json;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebApi.Handlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger,
                                    IServiceScopeFactory serviceScopeFactory) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogError($"Something went wrong: {exception}");
            await LogErrorAsync(httpContext, exception, cancellationToken);

        }
        finally
        {
            await HandleExceptionAsync(httpContext);
        }
        return true;
    }

    private async Task LogErrorAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        ITokenResolverService tokenResolverService = scope.ServiceProvider.GetRequiredService<ITokenResolverService>();
        IErrorLogService errorLogService = scope.ServiceProvider.GetRequiredService<IErrorLogService>();
        ConfigSettings configSettings = scope.ServiceProvider.GetRequiredService<ConfigSettings>();

        var request = httpContext.Request;

        var traceIdentifier = httpContext.TraceIdentifier;
        var clientIp = httpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();
        var path = request.Path + request.QueryString;
        string? stackTrace = exception.StackTrace?.Length > 2000 ? exception.StackTrace[..2000] : exception.StackTrace;
        var token = string.Empty;
        Guid? userId = null;

        var authHeaderName = configSettings.AuthSettings.HeaderName;

        if (!string.IsNullOrEmpty(request.Headers[authHeaderName]) &&
            request.Headers[authHeaderName].ToString().Length > 7)
        {
            token = request.Headers[authHeaderName].ToString();
            userId = !string.IsNullOrEmpty(token) ? tokenResolverService.GetUserIdFromToken() : null;
        }

        ErrorLogCreateDto errorLogToAddDto = new()
        {
            AccessToken = token,
            UserId = userId,
            Path = path,
            Ip = clientIp,
            ErrorMessage = exception.Message,
            StackTrace = stackTrace,
            TraceIdentifier = traceIdentifier
        };
        await errorLogService.AddAsync(errorLogToAddDto, cancellationToken);
    }

    private static async Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new ErrorResult(EMessages.GeneralError.Translate());
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
