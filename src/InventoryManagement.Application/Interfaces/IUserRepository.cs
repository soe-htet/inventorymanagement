using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUserNameAsync(string userName);
    Task AddAsync(User user);
    Task SaveChangesAsync();
}