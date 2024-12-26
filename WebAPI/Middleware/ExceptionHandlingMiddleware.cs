using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Newtonsoft.Json;

namespace WebAPI.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                using var scope = serviceScopeFactory.CreateScope();
                var exceptionLogRepository = scope.ServiceProvider.GetRequiredService<IExceptionLogRepository>();
                await HandleExceptionAsync(context, ex, exceptionLogRepository);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, IExceptionLogRepository exceptionLogRepository)
        {
            var exceptionLog = new ExceptionLog
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace,
                Source = exception.Source,
                Timestamp = DateTime.UtcNow,
                RequestPath = context.Request.Path,
                UserIdentifier = context.User.Identity?.Name
            };

            await exceptionLogRepository.LogAsync(exceptionLog);

            var response = new
            {
                error = exception is BaseException ? exception.Message : "An unexpected error occurred",
                code = exception is BaseException ? ((BaseException)exception).Code : "INTERNAL_SERVER_ERROR"
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                BaseException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
