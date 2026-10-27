using Moq;
using Xunit;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Services;

namespace InventoryManagement.Tests.Application;

public class ProductServiceTests
{
    [Fact]
    public async Task GetByIdAsync_WhenProductExists_ShouldReturnProduct()
    {
        // Arrange
        var product = new Product("Laptop", 1000m, 10, 10);

        var mockRepository = new Mock<IProductRepository>();
        var mockCategoryRepository = new Mock<ICategoryRepository>();


        mockRepository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(product);

        var service = new ProductService(
    mockRepository.Object,
    mockCategoryRepository.Object);

        // Act
        var result = await service.GetProductByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Laptop", result.Name);
        Assert.Equal(1000m,result.Price);
    }


    [Fact]
    public async Task GetByIdAsync_WhenProductNotExists_ShouldReturnNull()
    {
        // Arrange

        var mockRepository = new Mock<IProductRepository>();
        var mockCategoryRepository = new Mock<ICategoryRepository>();


        mockRepository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((Product?)null);

        var service = new ProductService(
        mockRepository.Object,
        mockCategoryRepository.Object);

        // Act
        var result = await service.GetProductByIdAsync(1);

        // Assert
        Assert.Null(result);
        mockRepository.Verify(r=>r.GetByIdAsync(1),Times.Once);
    }
}