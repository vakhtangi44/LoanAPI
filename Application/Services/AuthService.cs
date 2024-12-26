using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<(User User, string Token)> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
                throw new InvalidOperationException("Invalid credentials");

            if (!_passwordHasher.VerifyPassword(password, user.PasswordHash!))
                throw new InvalidOperationException("Invalid credentials");

            var token = await GenerateTokenAsync(user);
            return (user, token);
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            return await _jwtTokenGenerator.ValidateTokenAsync(token);
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            return await _jwtTokenGenerator.GenerateTokenAsync(user);
        }
    }
}
