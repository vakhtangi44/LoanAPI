using Application.DTOs;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface ILoanService
    {
        Task<LoanDto> CreateAsync(CreateLoanDto createDto, int userId);
        Task<LoanDto> UpdateStatusAsync(int loanId, LoanStatus status);
        Task<bool> DeleteAsync(int loanId, int userId);
        Task<LoanDto> GetByIdAsync(int loanId);
        Task<IEnumerable<LoanDto>> GetUserLoansAsync(int userId);
        Task<IEnumerable<LoanDto>> GetAllLoansAsync();
    }
}
