namespace PayMart.Infrastructure.Core.Services;

public static class ServicesURL
{
    public static string LoginUrl { get; set; } = "";
    public static string ClientUrl { get; set; } = "";
    public static string ProductUrl { get; set; } = "";
    public static string OrderUrl { get; set; } = "";
    public static string PaymentUrl { get; set; } = "";

    public static string Login(string Url, object Config = null)
    {
        if (Url == "registerUser")
            LoginUrl = "https://localhost:5000/api/Login/registerUser";

        if (Url == "getUser")
            LoginUrl = "https://localhost:5000/api/Login/getUser";

        return LoginUrl;
    }

    public static string Client(string Url, object Config = null)
    {
        if (Url == "getAll")
            ClientUrl = "https://localhost:5001/api/Client/getAll";

        if (Url == "getID")
            ClientUrl = "https://localhost:5001/api/Client/getID" + $"/{Config}";

        if (Url == "post")
            ClientUrl = "https://localhost:5001/api/Client/post";

        if (Url == "update")
            ClientUrl = "https://localhost:5001/api/Client/update" + $"/{Config}";

        if (Url == "delete")
            ClientUrl = "https://localhost:5001/api/Client/delete" + $"/{Config}";

        return ClientUrl;
    }

    public static string Product(string Url, object Config = null)
    {
        if (Url == "getAll")
            ProductUrl = "https://localhost:5002/api/Product/getAll";

        if (Url == "getID")
            ProductUrl = "https://localhost:5002/api/Product/getID" + $"/{Config}";

        if (Url == "post")
            ProductUrl = "https://localhost:5002/api/Product/post";

        if (Url == "update")
            ProductUrl = "https://localhost:5002/api/Product/update" + $"/{Config}";

        if (Url == "delete")
            ProductUrl = "https://localhost:5002/api/Product/delete" + $"/{Config}";

        return ProductUrl;
    }

    public static string Order(string Url, object Config = null)
    {
        if (Url == "getAll")
            OrderUrl = "https://localhost:5003/api/Order/getAll";

        if (Url == "getID")
            OrderUrl = "https://localhost:5003/api/Order/getID" + $"/{Config}";

        if (Url == "post")
            OrderUrl = "https://localhost:5003/api/Order/post";

        if (Url == "update")
            OrderUrl = "https://localhost:5003/api/Order/update" + $"/{Config}";

        if (Url == "delete")
            OrderUrl = "https://localhost:5003/api/Order/delete" + $"/{Config}";

        return OrderUrl;
    }

}
