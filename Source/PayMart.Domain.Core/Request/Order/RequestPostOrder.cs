namespace PayMart.Domain.Core.Request.Order;

public class RequestPostOrder
{
    public DateTime Date { get; set; }
    public string Name { get; set; } = "";
    public int UserID { get; set; }
    public int ProductID { get; set; }
}
