using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Age).InclusiveBetween(18, 100);
            RuleFor(x => x.MonthlyIncome).GreaterThan(0);
        }
    }
}
