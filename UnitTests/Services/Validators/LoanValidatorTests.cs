using Application.DTOs.LoanDtos;
using Application.Validators.LoanValidators;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace UnitTests.Services.Validators
{
    public class LoanValidatorTests
    {
        private readonly CreateLoanValidator _createValidator;
        private readonly UpdateLoanValidator _updateValidator;

        public LoanValidatorTests()
        {
            _createValidator = new CreateLoanValidator();
            _updateValidator = new UpdateLoanValidator();
        }

       

        [Fact]
        public void UpdateLoan_ValidData_PassesValidation()
        {
            // Arrange
            var loan = new UpdateLoanDto
            {
                Amount = 1000,
                Period = 12
            };

            // Act
            var result = _updateValidator.TestValidate(loan);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
