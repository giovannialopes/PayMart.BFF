using System.Text.Json.Serialization;

namespace PayMart.Domain.Core.Response.Order;

public class ResponsePostOrder
{
    public int ProductID { get; set; }
    public string Name { get; set; } = "";
    public DateTime Date { get; set; }

    [JsonIgnore]
    public int id { get; set; }
}
