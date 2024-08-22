namespace PayMart.Domain.Core.Request.Login;

public class RequestGetUserLogin
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}
