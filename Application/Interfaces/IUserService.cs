using Application.DTOs;
using Application.DTOs.Auth;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task AddAsync(RegisterRequest request);
        Task UpdateAsync(UserDto userDto);
        Task DeleteAsync(int id);
    }
}
