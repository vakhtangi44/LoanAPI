
using Domain.Enums;

namespace Application.DTOs
{
    public class LoanDto
    {
        public int Id { get; set; }
        public LoanType LoanType { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType Currency { get; set; }
        public int LoanPeriod { get; set; }
        public LoanStatus Status { get; set; }
        public int UserId { get; set; }
    }
}
