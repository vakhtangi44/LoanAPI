using System.Diagnostics;

namespace WebAPI.MIddlewares
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
            var stopwatch = Stopwatch.StartNew();
            await _next(context);
            stopwatch.Stop();

            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}, Response: {context.Response.StatusCode}, Duration: {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}