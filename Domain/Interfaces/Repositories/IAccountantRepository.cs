using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IAccountantRepository
    {
        Task<IEnumerable<Accountant>> GetAllAsync();
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task BlockUserAsync(int userId);
        Task UnblockUserAsync(int userId);
    }
}
