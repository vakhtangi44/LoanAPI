using Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static void SeedData(UserDbContext context)
        {
            if (context.Users.Any(u => u.Username == "GoatGb")) return;
            const string password = "GoatGb";
            var sha256 = SHA256.Create();
            var passwordHash = Convert.ToBase64String(
                sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            );

            var user = new User
            {
                Name = "Gela",
                Surname = "Barkalaia",
                Username = "GoatGb",
                Email = "gela.barkalaia@example.com",
                Age = 30,
                MonthlyIncome = 5000,
                PasswordHash = passwordHash,
                Role = "Accountant",
                CreatedAt = DateTime.UtcNow,
                IsBlocked = false
            };

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
