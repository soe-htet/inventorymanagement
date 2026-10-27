using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;


namespace InventoryManagement.Tests.Integration;

public class AuthIntegrationTests
{
    [Fact]
    public async Task Register_WithValidData_ShouldReturnCreated()
    {
        // Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var request = new
        {
            userName = "integrationuser",
            password = "Password123!"
        };

        // Act
        var response = await client.PostAsJsonAsync(
            "/api/auth/register",
            request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }


    [Fact]
    public async Task Register_WithDuplicateUsername_ShouldReturnConflict()
    {
        // Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var request = new
        {
            userName = "john",
            password = "Password123!"
        };

        var response = await client.PostAsJsonAsync(
        "/api/auth/register",
        request);
        var response2 = await client.PostAsJsonAsync(
        "/api/auth/register",
        request);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, response2.StatusCode);
    
    }

    [Fact]
    public async Task Login_WithValidCredentials_ShouldReturnJwtToken()
    {
        // Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var request = new
        {
            userName = "john",
            password = "Password123!"
        };

        var registerRes = await client.PostAsJsonAsync("/api/auth/register", request);

        var loginRes = await client.PostAsJsonAsync("/api/auth/login", request);

        var json = await loginRes.Content.ReadFromJsonAsync<JsonElement>();

        var token = json.GetProperty("accessToken").GetString();

        Assert.Equal(HttpStatusCode.Created, registerRes.StatusCode);
        Assert.Equal(HttpStatusCode.OK, loginRes.StatusCode);
        Assert.False(string.IsNullOrWhiteSpace(token));
    }


    [Fact]
    public async Task GetProducts_WithoutToken_ShouldReturnUnauthorized()
    {
        // Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var res = await client.GetAsync("/api/products");

        Assert.Equal(HttpStatusCode.Unauthorized, res.StatusCode);
    }


    [Fact(Skip = "JWT authentication issue - pending investigation")]
    public async Task GetProducts_WithValidToken_ShouldReturnOk()
    {
        // Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var request = new
        {
            userName = "john",
            password = "Password123!"
        };

        await client.PostAsJsonAsync("/api/auth/register", request);

        var loginRes = await client.PostAsJsonAsync(
            "/api/auth/login", request);

        var json = await loginRes.Content.ReadFromJsonAsync<JsonElement>();
        var token = json.GetProperty("accessToken").GetString();

        Console.WriteLine(token);

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var res = await client.GetAsync("/api/products");

        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
    }
}