using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository(UserDbContext context) : IUserRepository
    {
        public async Task<User> GetByIdAsync(int id)
        {
            return (await context.Users.FindAsync(id))!;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return (await context.Users.FirstOrDefaultAsync(u => u.Username == username))!;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (true)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> IsBlockedAsync(int id)
        {
            var user = await context.Users.FindAsync(id);
            return user is { IsBlocked: true };
        }
    }
}
