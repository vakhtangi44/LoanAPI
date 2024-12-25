using Application.DTOs.UserDtos;
using Application.Validators.UserValidators;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace UnitTests.Services.Validators
{
    public class UserValidatorTests
    {
        private readonly CreateUserValidator _createValidator;
        private readonly UpdateUserValidator _updateValidator;

        public UserValidatorTests()
        {
            _createValidator = new CreateUserValidator();
            _updateValidator = new UpdateUserValidator();
        }

        [Fact]
        public void CreateUser_ValidData_PassesValidation()
        {
            // Arrange
            var user = new CreateUserDto
            {
                Name = "John",
                Surname = "Doe",
                Username = "johndoe",
                Age = 30,
                Email = "john@example.com",
                MonthlyIncome = 5000,
                Password = "Password123",
                Role = Roles.User
            };

            // Act
            var result = _createValidator.TestValidate(user);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }


        [Fact]
        public void UpdateUser_ValidData_PassesValidation()
        {
            // Arrange
            var user = new UpdateUserDto
            {
                Name = "John",
                Surname = "Doe",
                Email = "john@example.com",
                MonthlyIncome = 5000
            };

            // Act
            var result = _updateValidator.TestValidate(user);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
