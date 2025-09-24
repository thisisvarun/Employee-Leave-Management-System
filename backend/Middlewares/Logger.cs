public class Logger
{
    private readonly RequestDelegate _next;

    public Logger(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
    }
}

public static class LoggerExtension
{
    public static IApplicationBuilder UseLogger(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<Logger>();
    }

}