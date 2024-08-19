namespace PayMart.Infrastructure.Core.Services;

public static class ServicesURL
{
    public static string ClientUrl { get; set; } = "";
    public static string ProductUrl { get; set; } = "";
    public static string OrderUrl { get; set; } = "";
    public static string PaymentUrl { get; set; } = "";


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


}
