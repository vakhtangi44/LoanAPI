using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ExceptionLogRepository(UserDbContext context) : IExceptionLogRepository
    {
        public async Task LogAsync(ExceptionLog exceptionLog)
        {
            await context.ExceptionLogs.AddAsync(exceptionLog);
            await context.SaveChangesAsync();
        }

        public async Task<ExceptionLog> GetByIdAsync(int id)
        {
            return (await context.ExceptionLogs.FindAsync(id))!;
        }
    }
}
