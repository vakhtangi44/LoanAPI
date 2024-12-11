using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;


namespace Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LoanService> _logger;

        public LoanService(
            ILoanRepository loanRepository,
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<LoanService> logger)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<LoanDto> CreateAsync(CreateLoanDto createDto, int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user.IsBlocked)
                throw new ApplicationException("User is blocked from creating loans");

            var loan = new Loan(
                createDto.LoanType,
                createDto.Amount,
                createDto.Currency,
                createDto.LoanPeriod,
                userId
            );

            await _loanRepository.AddAsync(loan);
            return _mapper.Map<LoanDto>(loan);
        }

        public async Task<LoanDto> UpdateStatusAsync(int loanId, LoanStatus status)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan == null)
                throw new ApplicationException("Loan not found");

            loan.Status = status;
            await _loanRepository.UpdateAsync(loan);

            return _mapper.Map<LoanDto>(loan);
        }

        public async Task<bool> DeleteAsync(int loanId, int userId)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan == null)
                throw new ApplicationException("Loan not found");

            if (loan.UserId != userId)
                throw new ApplicationException("Not authorized to delete this loan");

            if (loan.Status != LoanStatus.Processing)
                throw new ApplicationException("Can only delete loans in processing status");

            await _loanRepository.DeleteAsync(loanId);
            return true;
        }

        public async Task<LoanDto> GetByIdAsync(int loanId)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            return _mapper.Map<LoanDto>(loan);
        }

        public async Task<IEnumerable<LoanDto>> GetUserLoansAsync(int userId)
        {
            var loans = await _loanRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }

        public async Task<IEnumerable<LoanDto>> GetAllLoansAsync()
        {
            var loans = await _loanRepository.GetByUserIdAsync(0);
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }
    }
}