using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}