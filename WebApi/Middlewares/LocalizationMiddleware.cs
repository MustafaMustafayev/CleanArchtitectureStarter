using System.Globalization;
using WebApi.Helpers;

namespace WebApi.Middlewares;

public class LocalizationMiddleware(RequestDelegate context)
{
    private readonly RequestDelegate _next = context;

    public async Task Invoke(HttpContext context)
    {
        var requestLang = context.Request.Headers[LocalizationConstants.LANG_HEADER_NAME].ToString();

        var threadLang = requestLang switch
        {
            LocalizationConstants.LANG_HEADER_AZ => "az-Latn",
            LocalizationConstants.LANG_HEADER_EN => "en-GB",
            LocalizationConstants.LANG_HEADER_RU => "ru-RU",
            _ => "en-GB"
        };

        Thread.CurrentThread.CurrentCulture = new CultureInfo(threadLang);
        Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

        context.Items["ClientLang"] = threadLang;
        context.Items["ClientCulture"] = Thread.CurrentThread.CurrentUICulture.Name;

        LocalizationConstants.CurrentLang = requestLang ?? LocalizationConstants.DefaultLang;

        await _next.Invoke(context);
    }
}