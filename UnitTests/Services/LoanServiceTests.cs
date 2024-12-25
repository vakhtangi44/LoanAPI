using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Moq;

namespace UnitTests.Services
{


    public class LoanServiceTests
    {
        private readonly Mock<ILoanRepository> _loanRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly LoanService _loanService;

        public LoanServiceTests()
        {
            _loanRepositoryMock = new Mock<ILoanRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _loanService = new LoanService(_loanRepositoryMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetLoanByIdAsync_LoanExists_ReturnsLoan()
        {
            // Arrange
            var loanId = 1;
            var loan = new Loan { Id = loanId };

            _loanRepositoryMock.Setup(r => r.GetByIdAsync(loanId)).ReturnsAsync(loan);

            // Act
            var result = await _loanService.GetLoanByIdAsync(loanId);

            // Assert
            Assert.Equal(loan, result);
        }

        [Fact]
        public async Task GetLoanByIdAsync_LoanDoesNotExist_ThrowsLoanNotFoundException()
        {
            // Arrange
            var loanId = 1;

            _loanRepositoryMock.Setup(r => r.GetByIdAsync(loanId)).ReturnsAsync((Loan)null!);

            // Act & Assert
            await Assert.ThrowsAsync<LoanNotFoundException>(() => _loanService.GetLoanByIdAsync(loanId));
        }

        [Fact]
        public async Task GetUserLoansAsync_UserExists_ReturnsLoans()
        {
            // Arrange
            var userId = 1;
            var loans = new List<Loan> { new Loan { UserId = userId } };

            _userRepositoryMock.Setup(r => r.ExistsAsync(userId)).ReturnsAsync(true);
            _loanRepositoryMock.Setup(r => r.GetByUserIdAsync(userId)).ReturnsAsync(loans);

            // Act
            var result = await _loanService.GetUserLoansAsync(userId);

            // Assert
            Assert.Equal(loans, result);
        }

        [Fact]
        public async Task GetUserLoansAsync_UserDoesNotExist_ThrowsUserNotFoundException()
        {
            // Arrange
            var userId = 1;

            _userRepositoryMock.Setup(r => r.ExistsAsync(userId)).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _loanService.GetUserLoansAsync(userId));
        }

        [Fact]
        public async Task CreateLoanAsync_UserIsBlocked_ThrowsInvalidOperationException()
        {
            // Arrange
            var userId = 1;
            var loan = new Loan { UserId = userId };

            _userRepositoryMock.Setup(r => r.IsBlockedAsync(userId)).ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _loanService.CreateLoanAsync(loan));
        }

        [Fact]
        public async Task CreateLoanAsync_ValidLoan_CreatesLoan()
        {
            // Arrange
            var userId = 1;
            var loan = new Loan { UserId = userId, Amount = 1000, Period = 12 };
            var createdLoan = new Loan { Id = 1, UserId = userId, Amount = 1000, Period = 12, Status = LoanStatus.InProcess };

            _userRepositoryMock.Setup(r => r.IsBlockedAsync(userId)).ReturnsAsync(false);
            _loanRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Loan>())).ReturnsAsync(createdLoan);

            // Act
            var result = await _loanService.CreateLoanAsync(loan);

            // Assert
            Assert.Equal(createdLoan, result);
        }

        [Fact]
        public async Task UpdateLoanAsync_LoanNotInProcess_ThrowsInvalidOperationException()
        {
            // Arrange
            var loanId = 1;
            var existingLoan = new Loan { Id = loanId, Status = LoanStatus.Approved };

            _loanRepositoryMock.Setup(r => r.GetByIdAsync(loanId)).ReturnsAsync(existingLoan);

            var loanUpdate = new Loan { Amount = 2000, Period = 24 };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _loanService.UpdateLoanAsync(loanId, loanUpdate));
        }

        [Fact]
        public async Task DeleteLoanAsync_LoanNotInProcess_ThrowsInvalidOperationException()
        {
            // Arrange
            var loanId = 1;
            var loan = new Loan { Id = loanId, Status = LoanStatus.Approved };

            _loanRepositoryMock.Setup(r => r.GetByIdAsync(loanId)).ReturnsAsync(loan);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _loanService.DeleteLoanAsync(loanId));
        }

        [Fact]
        public async Task UpdateLoanStatusAsync_ValidLoan_UpdatesStatus()
        {
            // Arrange
            var loanId = 1;
            var loan = new Loan { Id = loanId, Status = LoanStatus.InProcess };

            _loanRepositoryMock.Setup(r => r.GetByIdAsync(loanId)).ReturnsAsync(loan);
            _loanRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Loan>())).ReturnsAsync(loan);

            // Act
            var result = await _loanService.UpdateLoanStatusAsync(loanId, LoanStatus.Approved);

            // Assert
            Assert.Equal(LoanStatus.Approved, result.Status);
        }

        [Fact]
        public async Task GetAllLoansAsync_ReturnsAllLoans()
        {
            // Arrange
            var loans = new List<Loan> { new Loan { Id = 1 }, new Loan { Id = 1 } };

            _loanRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(loans);

            // Act
            var result = await _loanService.GetAllLoansAsync();

            // Assert
            Assert.Equal(loans, result);
        }
    }
}
