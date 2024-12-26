using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LoanRepository(UserDbContext context) : ILoanRepository
    {
        public async Task<Loan> GetByIdAsync(int id)
        {
            return (await context.Loans.FindAsync(id))!;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await context.Loans.ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetByUserIdAsync(int userId)
        {
            return await context.Loans
                .Where(l => l.UserId == userId)
                .ToListAsync();
        }

        public async Task<Loan> CreateAsync(Loan loan)
        {
            await context.Loans.AddAsync(loan);
            await context.SaveChangesAsync();
            return loan;
        }

        public async Task<Loan> UpdateAsync(Loan loan)
        {
            context.Loans.Update(loan);
            await context.SaveChangesAsync();
            return loan;
        }

        public async Task DeleteAsync(int id)
        {
            var loan = await GetByIdAsync(id);
            context.Loans.Remove(loan);
            await context.SaveChangesAsync();
        }
    }
}
