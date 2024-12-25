using Domain.Enums;

namespace Application.DTOs.LoanDtos
{
    public class CreateLoanDto
    {
        public int Id { get; set; }
        public LoanType Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int Period { get; set; }
    }
}
