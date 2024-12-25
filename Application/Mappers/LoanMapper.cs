using Application.DTOs;
using Domain.Entities;
namespace Application.Mappers
{
    public static class LoanMapper
    {
        public static LoanDto ToDto(Loan loan) =>
            new LoanDto
            {
                Id = loan.Id,
                LoanType = loan.LoanType,
                Amount = loan.Amount,
                Currency = loan.Currency,
                LoanPeriod = loan.LoanPeriod,
                Status = loan.Status
            };

        public static Loan ToEntity(LoanDto dto, int userId) =>
            new Loan(dto.LoanType, dto.Amount, dto.Currency, dto.LoanPeriod, userId);
    }
}
