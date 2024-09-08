namespace PayMart.Domain.Core.Model;

public class ModelLogin
{
    public class RequestUserLogin
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }


    public class ResponsePostLogin
    {
        public string Token { get; set; } = string.Empty;
    }


}
