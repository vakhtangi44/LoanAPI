using Application.DTOs;

namespace Application.Services.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanDto>> GetLoansByUserIdAsync(int userId);
        Task<LoanDto> GetLoanByIdAsync(int loanId);
        Task AddLoanAsync(LoanDto loanDto, int userId);
        Task UpdateLoanAsync(LoanDto loanDto);
        Task DeleteLoanAsync(int loanId);
    }
}
