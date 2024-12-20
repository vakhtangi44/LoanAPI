using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountantDto>> GetAllAccountantsAsync();
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task BlockUserAsync(int userId);
        Task UnblockUserAsync(int userId);
    }
}
