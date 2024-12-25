using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ExceptionLogRepository : IExceptionLogRepository
    {
        private readonly UserDbContext _context;

        public ExceptionLogRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(ExceptionLog exceptionLog)
        {
            await _context.ExceptionLogs.AddAsync(exceptionLog);
            await _context.SaveChangesAsync();
        }

        public async Task<ExceptionLog> GetByIdAsync(Guid id)
        {
            return await _context.ExceptionLogs.FindAsync(id);
        }
    }
}
