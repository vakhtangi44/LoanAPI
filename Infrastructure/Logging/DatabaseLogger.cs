using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Logging
{
    public class DatabaseLogger
    {
        private readonly LoanDbContext _dbContext;

        public DatabaseLogger(LoanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task LogError(string message, string stackTrace)
        {
            var errorLog = new ErrorLog
            {
                Message = message,
                StackTrace = stackTrace,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.ErrorLogs.Add(errorLog);
            await _dbContext.SaveChangesAsync();
        }
    }
}
