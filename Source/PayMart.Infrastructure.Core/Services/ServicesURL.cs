namespace PayMart.Infrastructure.Core.Services;

public static class ServicesURL
{
    public static string ClientUrl { get; set; } = "";
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


        return ClientUrl;
    }


}
