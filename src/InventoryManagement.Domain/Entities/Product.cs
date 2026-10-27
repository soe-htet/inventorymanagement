namespace InventoryManagement.Domain.Entities;

public class Product
{
    public int Id{get; private set;}
    public string Name{get; private set;} = string.Empty;
    public decimal Price{get; set;}
    public int StockQuantity{get;set;}
    public int? CategoryId{get;set;}
    public Category? Category{get;set;}

    public Product(string name, decimal price, int stockQuantity, int? categoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Product Name is Required");
        }
        if(price < 0)
        {
            throw new ArgumentException("Price cannot be negative");
        }
        if(stockQuantity < 0)
        {
            throw new ArgumentException("Stock cannot be negative");
        }
        if(categoryId < 0)
        {
            throw new ArgumentException("Category Id is invalid");
        }
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
        CategoryId = categoryId;
    }

    public void ReduceStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.");

        if (quantity > StockQuantity)
            throw new InvalidOperationException("Insufficient stock.");

        StockQuantity -= quantity;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if(newPrice < 0)
        {
            throw new ArgumentException("Price cannot be negative");
        }
        Price = newPrice;
    }
}