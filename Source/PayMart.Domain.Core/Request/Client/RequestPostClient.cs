using System.Text.Json.Serialization;

namespace PayMart.Domain.Core.Request.Client;

public class RequestPostClient
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public int Age { get; set; }
    public string Address { get; set; } = "";

    [JsonIgnore]
    public int UserID { get; set; }
}
