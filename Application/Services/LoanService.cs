using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Mappers;
using Domain.Enums;
using Domain.Interfaces.Repositories;


namespace Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUserRepository _userRepository;

        public LoanService(ILoanRepository loanRepository, IUserRepository userRepository)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<LoanDto>> GetLoansByUserIdAsync(int userId)
        {
            var loans = await _loanRepository.GetByUserIdAsync(userId);
            return loans.Select(LoanMapper.ToDto);
        }

        public async Task<LoanDto> GetLoanByIdAsync(int loanId)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan == null)
                throw new NotFoundException($"Loan with ID {loanId} not found.");

            return LoanMapper.ToDto(loan);
        }

        public async Task AddLoanAsync(LoanDto loanDto, int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || user.IsBlocked)
            {
                throw new ValidationException(new Dictionary<string, string[]>
        {
            { "User", new[] { "User is not authorized to apply for a loan." } }
        });
            }

            var loan = LoanMapper.ToEntity(loanDto, userId);
            await _loanRepository.AddAsync(loan);
        }

        public async Task UpdateLoanAsync(LoanDto loanDto)
        {
            var loan = await _loanRepository.GetByIdAsync(loanDto.Id);
            if (loan == null || loan.Status != LoanStatus.Processing)
            {
                throw new ValidationException(new Dictionary<string, string[]>
        {
            { "Loan", new[] { "Loan cannot be updated." } }
        });
            }

            loan.LoanType = loanDto.LoanType;
            loan.Amount = loanDto.Amount;
            loan.Currency = loanDto.Currency;
            loan.LoanPeriod = loanDto.LoanPeriod;

            await _loanRepository.UpdateAsync(loan);
        }


        public async Task DeleteLoanAsync(int loanId)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan == null || loan.Status != LoanStatus.Processing)
            {
                throw new ValidationException(new Dictionary<string, string[]>
        {
            { "Loan", new[] { "Loan cannot be deleted." } }
        });
            }

            await _loanRepository.DeleteAsync(loanId);
        }
    }
}