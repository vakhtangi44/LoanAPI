using Domain.Enums;

namespace Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public LoanType LoanType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public int Period { get; set; }
        public LoanStatus Status { get; set; } = LoanStatus.InProcess;

        public User User { get; set; } = default!;
    }
}