using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Services;
using InventoryManagement.Domain.Entities;
using Moq;

namespace InventoryManagement.Tests.Application;

public class AuthServiceTests
{
    [Fact]
    public async Task RegisterAsync_WhenUsernameExists_ShouldReturnFalse()
    {
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();
        var mockPasswordHasher = new Mock<IPasswordHasher>();
        var mockJwtGenerator = new Mock<IJwtTokenGenerator>();

        var existingUser = new User(
            "john",
            "existing-hash");

        mockUserRepository
            .Setup(r => r.GetByUserNameAsync("john"))
            .ReturnsAsync(existingUser);

        var service = new AuthService(
            mockUserRepository.Object,
            mockPasswordHasher.Object,
            mockJwtGenerator.Object);

        var res = await service.RegisterAsync("john", "password123");
        
        Assert.False(res);

        mockUserRepository.Verify(r=>r.AddAsync(It.IsAny<User>()),Times.Never);
        mockUserRepository.Verify(r=>r.SaveChangesAsync(),Times.Never);
    }

    [Fact]
    public async Task RegisterAsync_WhenUsernameIsNew_ShouldReturnTrue()
    {
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();
        var mockPasswordHasher = new Mock<IPasswordHasher>();
        var mockJwtGenerator = new Mock<IJwtTokenGenerator>();

        mockUserRepository
            .Setup(r => r.GetByUserNameAsync("john1"))
            .ReturnsAsync((User?)null);

        mockPasswordHasher
            .Setup(p => p.HashPassword("Password123!"))
            .Returns("hashed-password");

        var service = new AuthService(
            mockUserRepository.Object,
            mockPasswordHasher.Object,
            mockJwtGenerator.Object);

        // Act
        var res = await service.RegisterAsync("john1", "Password123!");

        // Assert
        Assert.True(res);

        mockUserRepository.Verify(
            r => r.AddAsync(It.IsAny<User>()),
            Times.Once);

        mockUserRepository.Verify(
            r => r.SaveChangesAsync(),
            Times.Once);

        mockPasswordHasher.Verify(
            p => p.HashPassword("Password123!"),
            Times.Once);
    }
}