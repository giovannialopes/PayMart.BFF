using System.Text.Json.Serialization;

namespace PayMart.Domain.Core.Model;

public class ModelClient
{
    public class ClientRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
    }

    public class ClientResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
