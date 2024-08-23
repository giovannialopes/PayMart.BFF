using System.Text.Json.Serialization;

namespace PayMart.Domain.Core.Response.Order;

public class ResponsePostOrder
{
    public string Name { get; set; } = "";
    public decimal price { get; set; }


    [JsonIgnore]
    public int id { get; set; }

    [JsonIgnore]
    public int ProductID { get; set; }
}
