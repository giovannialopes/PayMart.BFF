namespace PayMart.Domain.Core.Model;

public class ModelOrder
{
    public class OrderRequest
    {
        public List<int> ProductIds { get; set; } = new List<int>();

    }

    public class OrderResponse
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
