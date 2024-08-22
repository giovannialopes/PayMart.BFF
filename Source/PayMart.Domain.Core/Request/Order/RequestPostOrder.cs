using System.Text.Json.Serialization;

namespace PayMart.Domain.Core.Request.Order;

public class RequestPostOrder
{
    public int ProductID { get; set; }
    public string Name { get; set; } = "";

}
