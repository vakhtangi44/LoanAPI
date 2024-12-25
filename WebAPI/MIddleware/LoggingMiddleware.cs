using Serilog;

namespace WebAPI.MIddleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Log.Information(
                "Request {Method} {Path} started",
                context.Request.Method,
                context.Request.Path
            );

            await _next(context);

            Log.Information(
                "Request {Method} {Path} completed with status code {StatusCode}",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode
            );
        }
    }
}
