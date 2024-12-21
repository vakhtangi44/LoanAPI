using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task<string> RefreshTokenAsync(string token);
        Task<RefreshToken> GenerateRefreshTokenAsync(int userId);
        Task<bool> ValidateRefreshTokenAsync(string token, int userId);
    }
}
