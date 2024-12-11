using Domain.Enums;

namespace Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public LoanType LoanType { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType Currency { get; set; }
        public int LoanPeriod { get; set; }
        public LoanStatus Status { get; set; } = LoanStatus.Processing;

        public int UserId { get; set; }

        public User User { get; set; }

        public Loan() { }

        public Loan(LoanType loanType, decimal amount, CurrencyType currency, int loanPeriod, int userId)
        {
            LoanType = loanType;
            Amount = amount;
            Currency = currency;
            LoanPeriod = loanPeriod;
            UserId = userId;
        }
    }
}