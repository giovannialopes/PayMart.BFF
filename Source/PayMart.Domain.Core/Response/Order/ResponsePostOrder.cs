namespace PayMart.Domain.Core.Response.Order;

public class ResponsePostOrder
{
    public DateTime Date { get; set; } = DateTime.Now;
    public string Name { get; set; } = "";
}
