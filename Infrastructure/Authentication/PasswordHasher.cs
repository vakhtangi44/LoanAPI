using Domain.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHashService
    {
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var providedPasswordHash = HashPassword(providedPassword);
            return hashedPassword == providedPasswordHash;
        }
    }
}