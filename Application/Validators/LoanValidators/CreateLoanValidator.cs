using Application.DTOs.LoanDtos;
using FluentValidation;

namespace Application.Validators.LoanValidators
{
    public class CreateLoanValidator : AbstractValidator<CreateLoanDto>
    {
        public CreateLoanValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Currency).NotEmpty().Length(3).Matches("^[A-Z]{3}$")
                .WithMessage("Currency must be a 3-letter ISO currency code");
            RuleFor(x => x.Period).NotEmpty().InclusiveBetween(1, 360)
                .WithMessage("Loan period must be between 1 and 360 months");
            RuleFor(x => x.Type).IsInEnum().NotEmpty();
        }
    }
}
