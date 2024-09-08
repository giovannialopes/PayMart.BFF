namespace PayMart.Infrastructure.Core.Services;

public static class ServicesURL
{
    private static readonly string Environment = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

    private static string LoginBaseUrl;
    private static string ClientBaseUrl;
    private static string ProductBaseUrl;
    private static string OrderBaseUrl;
    private static string PaymentBaseUrl;

    static ServicesURL()
    {
        if (Environment == "Development")
        {
            LoginBaseUrl = "http://localhost:5000/api/Login";
            ClientBaseUrl = "http://localhost:5001/api/Client";
            ProductBaseUrl = "http://localhost:5002/api/Product";
            OrderBaseUrl = "http://localhost:5003/api/Order";
            PaymentBaseUrl = "http://localhost:5004/api/Payment";
        }
        else
        {
            LoginBaseUrl = "http://paymart-login:5000/api/Login";
            ClientBaseUrl = "http://paymart-clients:5001/api/Client";
            ProductBaseUrl = "http://paymart-products:5002/api/Product";
            OrderBaseUrl = "http://paymart-orders:5003/api/Order";
            PaymentBaseUrl = "http://paymart-payments:5004/api/Payment";
        }
    }

    public enum UrlType
    {
        RegisterUser,
        GetUser,
        GetAll,
        GetID,
        Post,
        Update,
        Delete
    }

    public static string GetLoginUrl(UrlType type, object config = null!)
    {
        return type switch
        {
            UrlType.RegisterUser => $"{LoginBaseUrl}/registerUser",
            UrlType.GetUser => $"{LoginBaseUrl}/getUser",
            UrlType.Delete => $"{LoginBaseUrl}/delete/{config}",
            _ => LoginBaseUrl
        };
    }

    public static string GetClientUrl(UrlType type, object config = null!, object userID = null!)
    {
        return type switch
        {
            UrlType.GetAll => $"{ClientBaseUrl}/getAll",
            UrlType.GetID => $"{ClientBaseUrl}/getID/{config}",
            UrlType.Post => $"{ClientBaseUrl}/post/{config}",
            UrlType.Update => $"{ClientBaseUrl}/update/{config}/{userID}",
            UrlType.Delete => $"{ClientBaseUrl}/delete/{config}",
            _ => ClientBaseUrl
        };
    }

    public static string GetProductUrl(UrlType type, object config = null!, object userID = null!)
    {
        return type switch
        {
            UrlType.GetAll => $"{ProductBaseUrl}/getAll",
            UrlType.GetID => $"{ProductBaseUrl}/getID/{config}",
            UrlType.Post => $"{ProductBaseUrl}/post/{config}",
            UrlType.Update => $"{ProductBaseUrl}/update/{config}/{userID}",
            UrlType.Delete => $"{ProductBaseUrl}/delete/{config}",
            _ => ProductBaseUrl
        };
    }

    public static string GetOrderUrl(UrlType type, object config = null!, object userID = null!)
    {
        return type switch
        {
            UrlType.GetAll => $"{OrderBaseUrl}/getAll",
            UrlType.GetID => $"{OrderBaseUrl}/getID/{config}",
            UrlType.Post => $"{OrderBaseUrl}/post/{config}",
            UrlType.Update => $"{OrderBaseUrl}/update/{config}/{userID}",
            UrlType.Delete => $"{OrderBaseUrl}/delete/{config}",
            _ => OrderBaseUrl
        };
    }

    public static string GetPaymentUrl(UrlType type, object config = null!, object userID = null!)
    {
        return type switch
        {
            UrlType.GetAll => $"{PaymentBaseUrl}/getAll",
            UrlType.GetID => $"{PaymentBaseUrl}/getID/{config}",
            UrlType.Post => $"{PaymentBaseUrl}/post/{config}/{userID}",
            _ => PaymentBaseUrl
        };
    }

}
