using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence;

public static class DevelopmentAdminSeeder
{
    public static async Task SeedAsync(
        AppDbContext context,
        IPasswordHasher passwordHasher,
        string username,
        string password)
    {
        var exists = await context.Users
            .AnyAsync(u => u.UserName == username);

        if (exists)
            return;

        var passwordHash = passwordHasher.HashPassword(password);

        var admin = new User(username, passwordHash, "Admin");

        await context.Users.AddAsync(admin);
        await context.SaveChangesAsync();
    }
}