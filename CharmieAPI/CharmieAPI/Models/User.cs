using System.ComponentModel.DataAnnotations;

namespace CharmieAPI.Models;

public class User
{ 
    //Representa os dados dos utilizadors
    public string Id { get; set; }

    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    public virtual Identity Identity { get; set; }
}
