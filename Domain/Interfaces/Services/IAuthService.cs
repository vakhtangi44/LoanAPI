using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<(User User, string Token)> AuthenticateAsync(string username, string password);
        Task<bool> ValidateTokenAsync(string token);
        Task<string> GenerateTokenAsync(User user);
    }
}
