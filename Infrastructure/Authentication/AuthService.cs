using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUserRepository userRepository, JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || !new PasswordHasher().VerifyPassword(user.PasswordHash, password))
                throw new UnauthorizedAccessException("Invalid username or password.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.IsBlocked ? "BlockedUser" : "User"),
                new Claim("UserId", user.Id.ToString())
            };

            return _jwtTokenGenerator.GenerateToken(claims);
        }

        public async Task<string> RefreshTokenAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token) as JwtSecurityToken;

            if (jwtToken == null || jwtToken.ValidTo <= DateTime.UtcNow)
                throw new SecurityTokenException("Invalid token.");

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                throw new SecurityTokenException("Invalid token payload.");

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new UnauthorizedAccessException("User not found.");

            var refreshTokenValid = await ValidateRefreshTokenAsync(token, userId);
            if (!refreshTokenValid)
                throw new SecurityTokenException("Invalid refresh token.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.IsBlocked ? "BlockedUser" : "User"),
                new Claim("UserId", user.Id.ToString())
            };

            return _jwtTokenGenerator.GenerateToken(claims);
        }

        public Task<RefreshToken> GenerateRefreshTokenAsync(int userId)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.UtcNow.AddDays(7),
                UserId = userId
            };

            return Task.FromResult(refreshToken);
        }

        public async Task<bool> ValidateRefreshTokenAsync(string token, int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return false;

            var validRefreshToken = user.RefreshTokens.Any(rt => rt.Token == token && !rt.IsRevoked && rt.Expires > DateTime.UtcNow);

            return validRefreshToken;
        }
    }
}
