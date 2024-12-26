using Serilog;

namespace WebAPI.Middleware
{
    public class LoggingMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            Log.Information(
                "Request {Method} {Path} started",
                context.Request.Method,
                context.Request.Path
            );

            await next(context);

            Log.Information(
                "Request {Method} {Path} completed with status code {StatusCode}",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode
            );
        }
    }
}
