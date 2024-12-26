using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class UserService(IUserRepository userRepository, IPasswordHasher passwordHasher) : IUserService
    {
        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
                throw new UserNotFoundException(id);
            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.PasswordHash = passwordHasher.HashPassword(user.PasswordHash!);
            user.CreatedAt = DateTime.UtcNow;
            user.IsBlocked = false;
            return await userRepository.CreateAsync(user);
        }

        public async Task<User> UpdateUserAsync(int id, User userUpdate)
        {
            var user = await GetUserByIdAsync(id);
            user.Name = userUpdate.Name;
            user.Surname = userUpdate.Surname;
            user.Email = userUpdate.Email;
            user.MonthlyIncome = userUpdate.MonthlyIncome;
            user.UpdatedAt = DateTime.UtcNow;

            return await userRepository.UpdateAsync(user);
        }

        public async Task<User> BlockUserAsync(int id, DateTime until)
        {
            var user = await GetUserByIdAsync(id);
            user.IsBlocked = true;
            user.BlockedUntil = until;
            user.UpdatedAt = DateTime.UtcNow;

            return await userRepository.UpdateAsync(user);
        }

        public async Task<User> UnblockUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            user.IsBlocked = false;
            user.BlockedUntil = null;
            user.UpdatedAt = DateTime.UtcNow;

            return await userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await GetUserByIdAsync(id);
            await userRepository.DeleteAsync(id);
        }

        public async Task<bool> IsUserBlockedAsync(int id)
        {
            return await userRepository.IsBlockedAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await userRepository.GetAllAsync();
        }
    }
}
