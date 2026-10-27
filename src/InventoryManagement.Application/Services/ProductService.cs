using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Services;

public class ProductService
{

    private readonly IProductRepository _repository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(IProductRepository repository, ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
    }
    public async Task<Product> CreateProductAsync(string name, decimal price, int quantity, int? categoryId)
    {

        if (categoryId.HasValue)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId.Value);
            if(category is null)
            {
                throw new ArgumentException("Category does not exist");
            }
        }

        var product = new Product(name, price, quantity, categoryId);

        await _repository.AddAsync(product);
        await _repository.SaveChangeAsync();

        return product;
    }

    public async Task<List<ProductResponseDto>> GetAllProductsAsync()
    {
        var products = await _repository.GetAllAsync();
        return products.Select(MapToDto).ToList();
    }
    public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
    {
        var product= await _repository.GetByIdAsync(id);
        return product is null ? null : MapToDto(product);
    }

    public async Task<bool> UpdatePrice(int id, decimal newPrice)
    {
        var product = await _repository.GetByIdAsync(id);
        if(product == null)
        {
            return false;
        }

        product.UpdatePrice(newPrice);

        await _repository.SaveChangeAsync();
        return true;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product is null)
        {
            return false;
        }
        _repository.Remove(product);
        await _repository.SaveChangeAsync();
        return true;
    } 

    private static ProductResponseDto MapToDto(Product product)
    {
        var dto = new ProductResponseDto
        {
            Id= product.Id,
            Name=product.Name,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name
        };

        return dto;
    }
}