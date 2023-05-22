using System.ComponentModel.DataAnnotations;

namespace CharmieAPI.Models;

public class AuthenticationRequest
{
    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
