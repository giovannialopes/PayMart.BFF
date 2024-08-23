using System.Text.Json.Serialization;

namespace PayMart.Domain.Core.Response.Login;

public class ResponsePostLogin
{
    public string Token { get; set; } = "";

}
