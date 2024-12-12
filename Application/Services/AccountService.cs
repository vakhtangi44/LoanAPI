using Application.DTOs;
using Application.Exceptions;
using Application.Mappers;
using Application.Services.Interfaces;
using Domain.Interfaces;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountantRepository _accountantRepository;
        private readonly IUserRepository _userRepository;

        public AccountService(IAccountantRepository accountantRepository, IUserRepository userRepository)
        {
            _accountantRepository = accountantRepository;
            _userRepository = userRepository;
        }


        public async Task<IEnumerable<AccountantDto>> GetAllAccountantsAsync()
        {
            var accountants = await _accountantRepository.GetAllAsync();
            return accountants.Select(AccountantMapper.ToDto);
        }


        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _accountantRepository.GetAllUsersAsync();
            return users.Select(UserMapper.ToDto);
        }
        public async Task BlockUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new NotFoundException($"User with ID {userId} not found.");

            if (user.IsBlocked)
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "User", new[] { "User is already blocked." } }
                });

            await _accountantRepository.BlockUserAsync(userId);
        }
        public async Task UnblockUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new NotFoundException($"User with ID {userId} not found.");

            if (!user.IsBlocked)
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "User", new[] { "User is not blocked." } }
                });

            await _accountantRepository.UnblockUserAsync(userId);
        }
    }
}
