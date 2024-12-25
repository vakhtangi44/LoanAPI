using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(int id);
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<IEnumerable<Loan>> GetByUserIdAsync(int userId);
        Task<Loan> CreateAsync(Loan loan);
        Task<Loan> UpdateAsync(Loan loan);
        Task DeleteAsync(int id);
    }
}
