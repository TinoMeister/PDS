using System.ComponentModel.DataAnnotations;

namespace CharmieAPI.Models;

public class AuthenticationRequest
{
    //representar dados dos pedidos de entrada
    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
