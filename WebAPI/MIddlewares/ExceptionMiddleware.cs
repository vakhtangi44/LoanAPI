using Domain.Entities;
using Infrastructure.Persistence.Context;
using System.Net;
using System.Text.Json;

namespace WebAPI.MIddlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, LoanDbContext dbContext)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, dbContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, LoanDbContext dbContext, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Log error to database
            var errorLog = new ErrorLog
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                CreatedAt = DateTime.UtcNow
            };

            dbContext.ErrorLogs.Add(errorLog);
            await dbContext.SaveChangesAsync();

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An internal server error occurred. Please contact support.",
                Details = ex.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}