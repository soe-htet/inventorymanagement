using InventoryManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace InventoryManagement.Tests.Integration;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    private SqliteConnection? _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    ["Jwt:Key"] = "IntegrationTestOnlyKey_ChangeMe_12345678901234567890",
                    ["Jwt:Issuer"] = "InventoryManagement.Tests",
                    ["Jwt:Audience"] = "InventoryManagement.Tests"
                });
                
        });

        builder.ConfigureServices(services =>
        {
            // Remove the existing DbContext configuration
            services.RemoveAll<DbContextOptions<AppDbContext>>();
            services.RemoveAll<IDbContextOptionsConfiguration<AppDbContext>>();

            // Keep the SQLite in-memory connection open
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });

            // Create test database tables
            using var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();

            var context = scope.ServiceProvider
                .GetRequiredService<AppDbContext>();

            context.Database.EnsureCreated();
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _connection?.Dispose();
    }
}