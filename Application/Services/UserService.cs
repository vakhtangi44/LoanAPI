using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new UserNotFoundException(id);
            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user.PasswordHash);
            user.CreatedAt = DateTime.UtcNow;
            user.IsBlocked = false;
            return await _userRepository.CreateAsync(user);
        }

        public async Task<User> UpdateUserAsync(int id, User userUpdate)
        {
            var user = await GetUserByIdAsync(id);
            user.Name = userUpdate.Name;
            user.Surname = userUpdate.Surname;
            user.Email = userUpdate.Email;
            user.MonthlyIncome = userUpdate.MonthlyIncome;
            user.UpdatedAt = DateTime.UtcNow;

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<User> BlockUserAsync(int id, DateTime until)
        {
            var user = await GetUserByIdAsync(id);
            user.IsBlocked = true;
            user.BlockedUntil = until;
            user.UpdatedAt = DateTime.UtcNow;

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<User> UnblockUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            user.IsBlocked = false;
            user.BlockedUntil = null;
            user.UpdatedAt = DateTime.UtcNow;

            return await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await GetUserByIdAsync(id);
            await _userRepository.DeleteAsync(id);
        }

        public async Task<bool> IsUserBlockedAsync(int id)
        {
            return await _userRepository.IsBlockedAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}
