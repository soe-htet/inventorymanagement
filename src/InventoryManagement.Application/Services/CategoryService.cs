using InventoryManagement.Domain.Entities;
using InventoryManagement.Application.Interfaces;

namespace InventoryManagement.Application.Services;

public class CategoryService
{
    private readonly ICategoryRepository _repository;
    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Category> CreateCategoryAsync(string name)
    {
        var category = new Category(name);
        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();

        return category;
    }
    public async Task<List<Category>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if(category is null)
        {
            return false;
        }
        _repository.Remove(category);
        await _repository.SaveChangesAsync();
        return true;
    }
}