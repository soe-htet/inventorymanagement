using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagement.Infrastructure.Securities;

public class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<User> _hasher = new();
    public string HashPassword(string password)
    {
        return _hasher.HashPassword(null!, password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        var result = _hasher.VerifyHashedPassword(null!,passwordHash, password);

        return result != PasswordVerificationResult.Failed;
    }
}