using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;

namespace UnitTests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher>();
            _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            _authService = new AuthService(
                _userRepositoryMock.Object,
                _passwordHasherMock.Object,
                _jwtTokenGeneratorMock.Object
            );
        }

        [Fact]
        public async Task AuthenticateAsync_ValidCredentials_ReturnsUserAndToken()
        {
            // Arrange
            var username = "testuser";
            var password = "password";
            var user = new User { Username = username, PasswordHash = "hashedpassword" };
            var token = "generatedToken";

            _userRepositoryMock.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync(user);
            _passwordHasherMock.Setup(h => h.VerifyPassword(password, user.PasswordHash)).Returns(true);
            _jwtTokenGeneratorMock.Setup(t => t.GenerateTokenAsync(user)).ReturnsAsync(token);

            // Act
            var result = await _authService.AuthenticateAsync(username, password);

            // Assert
            Assert.Equal(user, result.User);
            Assert.Equal(token, result.Token);
        }

        [Fact]
        public async Task AuthenticateAsync_InvalidUsername_ThrowsInvalidOperationException()
        {
            // Arrange
            var username = "invaliduser";
            var password = "password";

            _userRepositoryMock.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _authService.AuthenticateAsync(username, password));
        }

        [Fact]
        public async Task AuthenticateAsync_InvalidPassword_ThrowsInvalidOperationException()
        {
            // Arrange
            var username = "testuser";
            var password = "wrongpassword";
            var user = new User { Username = username, PasswordHash = "hashedpassword" };

            _userRepositoryMock.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync(user);
            _passwordHasherMock.Setup(h => h.VerifyPassword(password, user.PasswordHash)).Returns(false);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _authService.AuthenticateAsync(username, password));
        }

        [Fact]
        public async Task ValidateTokenAsync_ValidToken_ReturnsTrue()
        {
            // Arrange
            var token = "validToken";

            _jwtTokenGeneratorMock.Setup(t => t.ValidateTokenAsync(token)).ReturnsAsync(true);

            // Act
            var result = await _authService.ValidateTokenAsync(token);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateTokenAsync_InvalidToken_ReturnsFalse()
        {
            // Arrange
            var token = "invalidToken";

            _jwtTokenGeneratorMock.Setup(t => t.ValidateTokenAsync(token)).ReturnsAsync(false);

            // Act
            var result = await _authService.ValidateTokenAsync(token);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GenerateTokenAsync_ValidUser_ReturnsToken()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Username = "testuser" };
            var token = "generatedToken";

            _jwtTokenGeneratorMock.Setup(t => t.GenerateTokenAsync(user)).ReturnsAsync(token);

            // Act
            var result = await _authService.GenerateTokenAsync(user);

            // Assert
            Assert.Equal(token, result);
        }
    }
}
