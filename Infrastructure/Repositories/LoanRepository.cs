using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly UserDbContext _context;

        public LoanRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<Loan> GetByIdAsync(int id)
        {
            return (await _context.Loans.FindAsync(id))!;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _context.Loans.ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetByUserIdAsync(int userId)
        {
            return await _context.Loans
                .Where(l => l.UserId == userId)
                .ToListAsync();
        }

        public async Task<Loan> CreateAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<Loan> UpdateAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task DeleteAsync(int id)
        {
            var loan = await GetByIdAsync(id);
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Loans.AnyAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Loan>> GetByStatusAsync(LoanStatus status)
        {
            return await _context.Loans
                .Where(l => l.Status == status)
                .ToListAsync();
        }
    }
}
