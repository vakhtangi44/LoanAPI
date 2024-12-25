using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IExceptionLogRepository
    {
        Task LogAsync(ExceptionLog exceptionLog);
        Task<ExceptionLog> GetByIdAsync(int id);
    }
}
