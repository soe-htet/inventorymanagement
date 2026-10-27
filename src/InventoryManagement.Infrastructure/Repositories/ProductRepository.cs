using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{

    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p=>p.Category)
            .AsNoTracking().ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.Include(p=>p.Category).FirstOrDefaultAsync(p=>p.Id==id);
    }

    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
    }
    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }
}