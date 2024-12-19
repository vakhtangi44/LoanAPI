using Application.DTOs;
using Application.Exceptions;
using Application.Mappers;
using Application.Services.Interfaces;
using Domain.Interfaces.Repositories;


namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(UserMapper.ToDto);
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new NotFoundException($"User with ID {id} not found.");
            return UserMapper.ToDto(user);
        }

        public async Task AddAsync(UserDto userDto)
        {
            var user = UserMapper.ToEntity(userDto);
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateAsync(UserDto userDto)
        {
            var user = UserMapper.ToEntity(userDto);
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}