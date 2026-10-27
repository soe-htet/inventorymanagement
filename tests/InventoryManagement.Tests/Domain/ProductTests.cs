using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Tests.Domain;

public class ProductTests
{
    [Fact]
    public void UpdatePrice_WithNegativePrice_ShouldThrowException()
    {
        // Arrange
        var product = new Product("Laptop",1000m, 10,1);

        Assert.Throws<ArgumentException>(()=>
        product.UpdatePrice(-100m));
    
        // When
    
        // Then
    }
    [Fact]
    public void UpdatePrice_WithValidPrice_ShouldUpdatePrice()
    {
        var product = new Product("Laptop",1000m, 10,10);
        product.UpdatePrice(150m);
        Assert.Equal(150m,product.Price);
    }
}