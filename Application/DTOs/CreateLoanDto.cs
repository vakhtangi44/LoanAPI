using Domain.Enums;

namespace Application.DTOs
{
    public class CreateLoanDto
    {
        public LoanType LoanType { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType Currency { get; set; }
        public int LoanPeriod { get; set; }
    }
}
