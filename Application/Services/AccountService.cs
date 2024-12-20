using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Mappers;
using Domain.Interfaces.Repositories;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountantRepository _accountantRepository;

        public AccountService(IAccountantRepository accountantRepository)
        {
            _accountantRepository = accountantRepository;
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
            await _accountantRepository.BlockUserAsync(userId);
        }

        public async Task UnblockUserAsync(int userId)
        {
            await _accountantRepository.UnblockUserAsync(userId);
        }
    }
}
