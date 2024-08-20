namespace PayMart.Domain.Core.Request.Product;

public class RequestPostProduct
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public int UserID { get; set; }
}
