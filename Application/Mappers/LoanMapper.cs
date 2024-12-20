using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class LoanMapper
    {
        public static LoanDto ToDto(Loan loan)
        {
            return new LoanDto
            {
                Id = loan.Id,
                LoanType = loan.LoanType,
                Amount = loan.Amount,
                Currency = loan.Currency,
                LoanPeriod = loan.LoanPeriod,
                Status = loan.Status
            };
        }

        public static Loan ToEntity(LoanDto dto, int userId)
        {
            return new Loan
            {
                Id = dto.Id,
                LoanType = dto.LoanType,
                Amount = dto.Amount,
                Currency = dto.Currency,
                LoanPeriod = dto.LoanPeriod,
                Status = dto.Status,
                UserId = userId
            };
        }
    }
}