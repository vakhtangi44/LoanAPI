using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;


namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository,
            IPasswordHashService passwordHashService,
            ITokenService tokenService,
            IMapper mapper,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
            _tokenService = tokenService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto> RegisterAsync(UserRegistrationDto registrationDto)
        {
            var hashedPassword = _passwordHashService.HashPassword(registrationDto.Password);

            var user = new User(
                registrationDto.FirstName,
                registrationDto.LastName,
                registrationDto.UserName,
                registrationDto.Email,
                registrationDto.Age,
                registrationDto.MonthlyIncome,
                hashedPassword
            );

            await _userRepository.AddAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<string> LoginAsync(UserLoginDto loginDto)
        {
            var user = await _userRepository.GetByIdAsync(loginDto.);
            if (user == null)
                throw new ApplicationException("User not found");

            var isBlocked = await _userRepository.IsUserBlockedAsync(user.Id);
            if (isBlocked)
                throw new ApplicationException("Account is blocked");

            if (!_passwordHashService.VerifyPassword(loginDto.Password, user.PasswordHash))
                throw new ApplicationException("Invalid credentials");

            return _tokenService.GenerateToken(user, "User");
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new ApplicationException("User not found");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> UpdateAsync(int userId, UpdateUserDto updateDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new ApplicationException("User not found");

            user.FirstName = updateDto.FirstName;
            user.LastName = updateDto.LastName;
            user.Age = updateDto.Age;
            user.MonthlyIncome = updateDto.MonthlyIncome;

            await _userRepository.UpdateAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            await _userRepository.DeleteAsync(userId);
            return true;
        }

        public async Task<bool> BlockUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new ApplicationException("User not found");

            user.IsBlocked = true;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> UnblockUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new ApplicationException("User not found");

            user.IsBlocked = false;
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
