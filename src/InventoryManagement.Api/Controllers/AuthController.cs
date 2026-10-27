using InventoryManagement.Api.Contracts.Users;
using InventoryManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{
    private readonly AuthService _authService;
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRequest request)
    {
        var registered = await _authService.RegisterAsync(request.UserName, request.Password);
        if (!registered)
        {
            return Conflict();
        }
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _authService.LoginAsync(request.UserName, request.Password);
        if (result is null)
        {
            return Unauthorized();
        }
        return Ok(new {accessToken=result});
    }
}