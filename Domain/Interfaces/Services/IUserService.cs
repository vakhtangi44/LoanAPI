using Domain.Entities;
namespace Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(Guid id, User user);
        Task DeleteUserAsync(Guid id);
        Task<User> BlockUserAsync(Guid id, DateTime until);
        Task<User> UnblockUserAsync(Guid id);
        Task<bool> IsUserBlockedAsync(Guid id);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
