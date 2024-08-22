namespace PayMart.Domain.Core.Request.Login;

public class RequestRegisterUserLogin
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}
