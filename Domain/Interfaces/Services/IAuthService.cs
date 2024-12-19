namespace Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task<string> RefreshTokenAsync(string token);
    }
}
