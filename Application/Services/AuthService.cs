using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class AuthService(IUserRepository userRepository,IPasswordHasher passwordHasher,
                            IJwtTokenGenerator jwtTokenGenerator) : IAuthService
    {
        public async Task<(User User, string Token)> AuthenticateAsync(string username, string password)
        {
            var user = await userRepository.GetByUsernameAsync(username);
            if (user == null)
                throw new InvalidOperationException("Invalid credentials");

            if (!passwordHasher.VerifyPassword(password, user.PasswordHash!))
                throw new InvalidOperationException("Invalid credentials");

            var token = await GenerateTokenAsync(user);
            return (user, token);
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            return await jwtTokenGenerator.ValidateTokenAsync(token);
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            return await jwtTokenGenerator.GenerateTokenAsync(user);
        }
    }
}
