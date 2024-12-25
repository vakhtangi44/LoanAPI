using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces.Services
{
    public interface ILoanService
    {
        Task<Loan> GetLoanByIdAsync(Guid id);
        Task<IEnumerable<Loan>> GetUserLoansAsync(Guid userId);
        Task<Loan> CreateLoanAsync(Loan loan);
        Task<Loan> UpdateLoanAsync(Guid id, Loan loan);
        Task DeleteLoanAsync(Guid id);
        Task<Loan> UpdateLoanStatusAsync(Guid id, LoanStatus status);
        Task<IEnumerable<Loan>> GetAllLoansAsync();
    }
}
