using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAccountantRepository
    {
        Task<IEnumerable<Accountant>> GetAllAsync();
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task BlockUserAsync(int userId);
        Task UnblockUserAsync(int userId);
    }
}
