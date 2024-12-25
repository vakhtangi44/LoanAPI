using Application.DTOs.UserDtos;
using FluentValidation;

namespace Application.Validators.UserValidators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.MonthlyIncome)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
