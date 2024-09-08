using PayMart.Domain.Core.Model;
using System.Text.Json;
using static PayMart.Domain.Core.Model.ModelClient;
using static PayMart.Domain.Core.Model.ModelOrder;
using static PayMart.Domain.Core.Model.ModelPayment;
using static PayMart.Domain.Core.Model.ModelProduct;

namespace PayMart.API.Core.Utilities;

public class JsonFormatter
{
    public class FormatterClient
    {
        public static string FormatterGetAll(string json)
        {
            using var jsonDocument = JsonDocument.Parse(json);
            var clientsArray = jsonDocument.RootElement.GetProperty("clients");

            var clientResponses = clientsArray.EnumerateArray()
                .Select(client => new ClientResponse
                {
                    Id = client.GetProperty("id").GetInt32(),
                    FullName = client.GetProperty("fullName").GetString() ?? string.Empty,
                    ContactEmail = client.GetProperty("contactEmail").GetString() ?? string.Empty,
                    PhoneNumber = client.GetProperty("phoneNumber").GetString() ?? string.Empty,
                    Age = client.GetProperty("age").GetInt32(),
                    Address = client.GetProperty("address").GetString() ?? string.Empty
                })
                .ToList();

            return JsonSerializer.Serialize(clientResponses, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        }

        public static string FormatterGetByID(string json)
        {
            using var jsonDocument = JsonDocument.Parse(json);
            var id = jsonDocument.RootElement;

            var idResponses = new ClientResponse
            {
                Id = id.GetProperty("id").GetInt32(),
                FullName = id.GetProperty("fullName").GetString() ?? string.Empty,
                ContactEmail = id.GetProperty("contactEmail").GetString() ?? string.Empty,
                PhoneNumber = id.GetProperty("phoneNumber").GetString() ?? string.Empty,
                Age = id.GetProperty("age").GetInt32(),
                Address = id.GetProperty("address").GetString() ?? string.Empty
            };

            return JsonSerializer.Serialize(idResponses, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        }
    }

    public class FormatterProduct
    {
        public static string FormatterGetAll(string json)
        {
            using var jsonDocument = JsonDocument.Parse(json);
            var productsArray = jsonDocument.RootElement.GetProperty("products");

            var productsResponses = productsArray.EnumerateArray()
                .Select(product => new ProductResponse
                {
                    Id = product.GetProperty("id").GetInt32(),
                    Name = product.GetProperty("name").GetString() ?? string.Empty,
                    Description = product.GetProperty("description").GetString() ?? string.Empty,
                    StockQuantity = product.GetProperty("stockQuantity").GetInt32(),
                    Price = product.GetProperty("price").GetDecimal(),
                    Status = product.GetProperty("status").GetString() ?? string.Empty,
                })
                .ToList();

            return JsonSerializer.Serialize(productsResponses, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        }

        public static string FormatterGetByID(string json)
        {
            using var jsonDocument = JsonDocument.Parse(json);
            var product = jsonDocument.RootElement;

            var idResponses = new ProductResponse
            {
                Id = product.GetProperty("id").GetInt32(),
                Name = product.GetProperty("name").GetString() ?? string.Empty,
                Description = product.GetProperty("description").GetString() ?? string.Empty,
                StockQuantity = product.GetProperty("stockQuantity").GetInt32(),
                Price = product.GetProperty("price").GetDecimal(),
                Status = product.GetProperty("status").GetString() ?? string.Empty,
            };

            return JsonSerializer.Serialize(idResponses, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        }
    }

    public class FormatterOrder
    {
        public static string FormatterGetAll(string json)
        {
            using var jsonDocument = JsonDocument.Parse(json);
            var ordersArray = jsonDocument.RootElement.GetProperty("orders");

            var ordersResponses = ordersArray.EnumerateArray()
                .Select(order => new OrderResponse
                {
                    Id = order.GetProperty("id").GetInt32(),
                    OrderDate = order.GetProperty("orderDate").GetDateTime(),
                    Price = order.GetProperty("price").GetDecimal(),
                    ProductId = order.GetProperty("productId").GetString() ?? string.Empty,
                    Status = order.GetProperty("status").GetString() ?? string.Empty,
                })
                .ToList();

            return JsonSerializer.Serialize(ordersResponses, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        }

        public static string FormatterGetByID(string json)
        {
            using var jsonDocument = JsonDocument.Parse(json);
            var order = jsonDocument.RootElement;

            var ordersResponses = new OrderResponse
            {
                Id = order.GetProperty("id").GetInt32(),
                OrderDate = order.GetProperty("orderDate").GetDateTime(),
                Price = order.GetProperty("price").GetDecimal(),
                ProductId = order.GetProperty("productId").GetString() ?? string.Empty,
                Status = order.GetProperty("status").GetString() ?? string.Empty,
            };

            return JsonSerializer.Serialize(ordersResponses, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        }
    }

    public class FormatterPayment
    {
        public static string FormatterGetAll(string json)
        {
            using var jsonDocument = JsonDocument.Parse(json);
            var paymentsArray = jsonDocument.RootElement.GetProperty("payments");

            var paymentsResponses = paymentsArray.EnumerateArray()
                .Select(payment => new PaymentResponse
                {
                    Id = payment.GetProperty("id").GetInt32(),
                    PaymentDate = payment.GetProperty("paymentDate").GetDateTime(),
                    AmountPaid = payment.GetProperty("amountPaid").GetDecimal(),
                    PaymentMethod = payment.GetProperty("paymentMethod").GetString() ?? string.Empty,
                    Status = payment.GetProperty("status").GetString() ?? string.Empty,
                    Protocol = payment.GetProperty("protocol").GetString() ?? string.Empty,
                })
                .ToList();

            return JsonSerializer.Serialize(paymentsResponses, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        }

        public static string FormatterGetByID(string json)
        {
            using var jsonDocument = JsonDocument.Parse(json);
            var payment = jsonDocument.RootElement;

            var paymentsResponses = new PaymentResponse
            {
                Id = payment.GetProperty("id").GetInt32(),
                PaymentDate = payment.GetProperty("paymentDate").GetDateTime(),
                AmountPaid = payment.GetProperty("amountPaid").GetDecimal(),
                PaymentMethod = payment.GetProperty("paymentMethod").GetString() ?? string.Empty,
                Status = payment.GetProperty("status").GetString() ?? string.Empty,
                Protocol = payment.GetProperty("protocol").GetString() ?? string.Empty,
            };

            return JsonSerializer.Serialize(paymentsResponses, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        }
    }

}
