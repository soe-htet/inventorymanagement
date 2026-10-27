using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces;

public interface ICategoryRepository
{
    Task AddAsync(Category category);
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    void Remove(Category category);
    Task SaveChangesAsync();

}