using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CreateLoanValidator : AbstractValidator<CreateLoanDto>
    {
        public CreateLoanValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0");

            RuleFor(x => x.LoanPeriod)
                .GreaterThan(0)
                .WithMessage("Loan period must be greater than 0");

            RuleFor(x => x.LoanType)
                .IsInEnum()
                .WithMessage("Invalid loan type");

            RuleFor(x => x.Currency)
                .IsInEnum()
                .WithMessage("Invalid currency type");
        }
    }
}
