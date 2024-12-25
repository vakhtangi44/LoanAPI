using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateTokenAsync(User user);
        Task<bool> ValidateTokenAsync(string token);
    }
}
