using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class LoanValidator : AbstractValidator<LoanDto>
    {
        public LoanValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.LoanPeriod).InclusiveBetween(1, 60);
            RuleFor(x => x.LoanType).IsInEnum();
            RuleFor(x => x.Currency).IsInEnum();
        }
    }
}
