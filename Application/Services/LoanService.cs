using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;


namespace Application.Services
{
    public class LoanService(ILoanRepository loanRepository, IUserRepository userRepository) : ILoanService
    {
        public async Task<Loan> GetLoanByIdAsync(int id)
        {
            var loan = await loanRepository.GetByIdAsync(id);
            if (loan == null)
                throw new LoanNotFoundException(id);
            return loan;
        }

        public async Task<IEnumerable<Loan>> GetUserLoansAsync(int userId)
        {
            if (!await userRepository.ExistsAsync(userId))
                throw new UserNotFoundException(userId);

            return await loanRepository.GetByUserIdAsync(userId);
        }

        public async Task<Loan> CreateLoanAsync(Loan loan)
        {
            if (await userRepository.IsBlockedAsync(loan.UserId))
                throw new InvalidOperationException("User is blocked from creating loans");

            loan.Status = LoanStatus.InProcess;
            loan.CreatedAt = DateTime.UtcNow;

            return await loanRepository.CreateAsync(loan);
        }

        public async Task<Loan> UpdateLoanAsync(int id, Loan loanUpdate)
        {
            var loan = await GetLoanByIdAsync(id);

            if (loan.Status != LoanStatus.InProcess)
                throw new InvalidOperationException("Can only update loans that are in process");

            loan.Amount = loanUpdate.Amount;
            loan.Period = loanUpdate.Period;
            loan.UpdatedAt = DateTime.UtcNow;

            return await loanRepository.UpdateAsync(loan);
        }

        public async Task<Loan> UpdateLoanStatusAsync(int id, LoanStatus status)
        {
            var loan = await GetLoanByIdAsync(id);
            loan.Status = status;
            loan.UpdatedAt = DateTime.UtcNow;

            return await loanRepository.UpdateAsync(loan);
        }

        public async Task DeleteLoanAsync(int id)
        {
            var loan = await GetLoanByIdAsync(id);

            if (loan.Status != LoanStatus.InProcess)
                throw new InvalidOperationException("Can only delete loans that are in process");

            await loanRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        {
            return await loanRepository.GetAllAsync();
        }
    }
}
