using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces.Services
{
    public interface ILoanService
    {
        Task<Loan> GetLoanByIdAsync(int id);
        Task<IEnumerable<Loan>> GetUserLoansAsync(int userId);
        Task<Loan> CreateLoanAsync(Loan loan);
        Task<Loan> UpdateLoanAsync(int id, Loan loan);
        Task DeleteLoanAsync(int id);
        Task<Loan> UpdateLoanStatusAsync(int id, LoanStatus status);
        Task<IEnumerable<Loan>> GetAllLoansAsync();
    }
}
