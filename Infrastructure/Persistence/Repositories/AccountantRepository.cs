using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class AccountantRepository : IAccountantRepository
    {
        private readonly LoanDbContext _dbContext;

        public AccountantRepository(LoanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Accountant>> GetAllAsync()
        {
            return await _dbContext.Accountants.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task BlockUserAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                user.IsBlocked = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UnblockUserAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                user.IsBlocked = false;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}