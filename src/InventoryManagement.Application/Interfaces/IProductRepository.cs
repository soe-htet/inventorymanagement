using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> GetAllAsync();
    Task AddAsync(Product product);
    void Remove(Product product);
    Task SaveChangeAsync();
}