using System.Text.Json.Serialization;

namespace PayMart.Domain.Core.Model;

public class ModelProduct
{

    public class ProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
    }


    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = string.Empty;
    }

}
