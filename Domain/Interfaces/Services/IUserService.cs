using Domain.Entities;
namespace Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(int id, User user);
        Task DeleteUserAsync(int id);
        Task<User> BlockUserAsync(int id, DateTime until);
        Task<User> UnblockUserAsync(int id);
        Task<bool> IsUserBlockedAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
