using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(int id);
        Task<IEnumerable<Loan>> GetByUserIdAsync(int userId);
        Task AddAsync(Loan loan);
        Task UpdateAsync(Loan loan);
        Task DeleteAsync(int id);
    }
}
