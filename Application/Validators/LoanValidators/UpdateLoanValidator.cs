using Application.DTOs.LoanDtos;
using FluentValidation;

namespace Application.Validators.LoanValidators
{
    public class UpdateLoanValidator : AbstractValidator<UpdateLoanDto>
    {
        public UpdateLoanValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0).WithMessage("Amount must be greater than 0");
            RuleFor(x => x.Period).NotEmpty().InclusiveBetween(1, 360)
                .WithMessage("Loan period must be between 1 and 360 months");
        }
    }
}
