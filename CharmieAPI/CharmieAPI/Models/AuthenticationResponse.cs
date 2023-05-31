namespace CharmieAPI.Models;

public class AuthenticationResponse
{   
    //representa os dados das respostas de saida
    public string Token { get; set; }

    public DateTime Expiration { get; set; }
}
