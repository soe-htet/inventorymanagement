using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Api.Contracts.Users;

public class UserRequest
{
    [Required]
    [StringLength(50,MinimumLength =3)]
    public string UserName{get; set;} = string.Empty;
    [Required]
    [MinLength(8)]
    public string Password{get;set;} = string.Empty;
}