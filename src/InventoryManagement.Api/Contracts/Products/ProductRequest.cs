namespace InventoryManagement.Api.Contracts.Products;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int? CategoryId{get;set;}
}