using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;


namespace Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LoanService(
            ILoanRepository loanRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Loan> GetLoanByIdAsync(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
                throw new LoanNotFoundException(id);
            return loan;
        }

        public async Task<IEnumerable<Loan>> GetUserLoansAsync(int userId)
        {
            if (!await _userRepository.ExistsAsync(userId))
                throw new UserNotFoundException(userId);

            return await _loanRepository.GetByUserIdAsync(userId);
        }

        public async Task<Loan> CreateLoanAsync(Loan loan)
        {
            if (await _userRepository.IsBlockedAsync(loan.UserId))
                throw new InvalidOperationException("User is blocked from creating loans");

            loan.Status = LoanStatus.InProcess;
            loan.CreatedAt = DateTime.UtcNow;

            return await _loanRepository.CreateAsync(loan);
        }

        public async Task<Loan> UpdateLoanAsync(int id, Loan loanUpdate)
        {
            var loan = await GetLoanByIdAsync(id);

            if (loan.Status != LoanStatus.InProcess)
                throw new InvalidOperationException("Can only update loans that are in process");

            loan.Amount = loanUpdate.Amount;
            loan.Period = loanUpdate.Period;
            loan.UpdatedAt = DateTime.UtcNow;

            return await _loanRepository.UpdateAsync(loan);
        }

        public async Task<Loan> UpdateLoanStatusAsync(int id, LoanStatus status)
        {
            var loan = await GetLoanByIdAsync(id);
            loan.Status = status;
            loan.UpdatedAt = DateTime.UtcNow;

            return await _loanRepository.UpdateAsync(loan);
        }

        public async Task DeleteLoanAsync(int id)
        {
            var loan = await GetLoanByIdAsync(id);

            if (loan.Status != LoanStatus.InProcess)
                throw new InvalidOperationException("Can only delete loans that are in process");

            await _loanRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        {
            return await _loanRepository.GetAllAsync();
        }
    }
}
