namespace WebApi.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate context)
    {
        _next = context;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);
    }
}