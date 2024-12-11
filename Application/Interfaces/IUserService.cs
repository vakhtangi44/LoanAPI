using Application.DTOs;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(UserRegistrationDto registrationDto);
        Task<string> LoginAsync(UserLoginDto loginDto);
        Task<UserDto> GetByIdAsync(int id);
        Task<bool> BlockUserAsync(int userId, DateTime until);
        Task<bool> UnblockUserAsync(int userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
