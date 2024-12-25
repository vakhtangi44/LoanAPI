using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;

namespace UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_userRepositoryMock.Object, _passwordHasherMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetUserByIdAsync_UserExists_ReturnsUser()
        {
            // Arrange
            var userId = int.Newint();
            var user = new User { Id = userId };

            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetUserByIdAsync_UserDoesNotExist_ThrowsUserNotFoundException()
        {
            // Arrange
            var userId = int.Newint();

            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _userService.GetUserByIdAsync(userId));
        }

        [Fact]
        public async Task CreateUserAsync_ValidUser_CreatesUser()
        {
            // Arrange
            var user = new User { PasswordHash = "plainPassword" };
            var hashedPassword = "hashedPassword";
            var createdUser = new User { Id = int.Newint(), PasswordHash = hashedPassword };

            _passwordHasherMock.Setup(h => h.HashPassword(user.PasswordHash)).Returns(hashedPassword);
            _userRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<User>())).ReturnsAsync(createdUser);

            // Act
            var result = await _userService.CreateUserAsync(user);

            // Assert
            Assert.Equal(createdUser, result);
            Assert.Equal(hashedPassword, result.PasswordHash);
        }

        [Fact]
        public async Task UpdateUserAsync_ValidUser_UpdatesUser()
        {
            // Arrange
            var userId = int.Newint();
            var existingUser = new User { Id = userId };
            var userUpdate = new User { Name = "UpdatedName", Email = "updated@example.com" };

            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(existingUser);
            _userRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<User>())).ReturnsAsync(existingUser);

            // Act
            var result = await _userService.UpdateUserAsync(userId, userUpdate);

            // Assert
            Assert.Equal(userUpdate.Name, result.Name);
            Assert.Equal(userUpdate.Email, result.Email);
        }

        [Fact]
        public async Task BlockUserAsync_ValidUser_BlocksUser()
        {
            // Arrange
            var userId = int.Newint();
            var blockUntil = DateTime.UtcNow.AddDays(1);
            var user = new User { Id = userId };

            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _userService.BlockUserAsync(userId, blockUntil);

            // Assert
            Assert.True(result.IsBlocked);
            Assert.Equal(blockUntil, result.BlockedUntil);
        }

        [Fact]
        public async Task UnblockUserAsync_ValidUser_UnblocksUser()
        {
            // Arrange
            var userId = int.Newint();
            var user = new User { Id = userId, IsBlocked = true, BlockedUntil = DateTime.UtcNow.AddDays(1) };

            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _userService.UnblockUserAsync(userId);

            // Assert
            Assert.False(result.IsBlocked);
            Assert.Null(result.BlockedUntil);
        }

        [Fact]
        public async Task DeleteUserAsync_ValidUser_DeletesUser()
        {
            // Arrange
            var userId = int.Newint();
            var user = new User { Id = userId };

            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act
            await _userService.DeleteUserAsync(userId);

            // Assert
            _userRepositoryMock.Verify(r => r.DeleteAsync(userId), Times.Once);
        }

        [Fact]
        public async Task IsUserBlockedAsync_UserExists_ReturnsBlockedStatus()
        {
            // Arrange
            var userId = int.Newint();
            var isBlocked = true;

            _userRepositoryMock.Setup(r => r.IsBlockedAsync(userId)).ReturnsAsync(isBlocked);

            // Act
            var result = await _userService.IsUserBlockedAsync(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsAllUsers()
        {
            // Arrange
            var users = new List<User> { new User { Id = int.Newint() }, new User { Id = int.Newint() } };

            _userRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.Equal(users, result);
        }
    }
}
