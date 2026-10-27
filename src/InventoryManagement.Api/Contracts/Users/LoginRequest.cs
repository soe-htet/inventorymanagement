using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Api.Contracts.Users;

public class LoginRequest
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}