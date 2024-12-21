using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LoanDbContext _dbContext;

        public LoanRepository(LoanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Loan> GetByIdAsync(int id)
        {
            return await _dbContext.Loans.Include(l => l.User).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Loan>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Loans.Where(l => l.UserId == userId).ToListAsync();
        }

        public async Task AddAsync(Loan loan)
        {
            _dbContext.Loans.Add(loan);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Loan loan)
        {
            _dbContext.Loans.Update(loan);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var loan = await _dbContext.Loans.FindAsync(id);
            if (loan != null)
            {
                _dbContext.Loans.Remove(loan);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
