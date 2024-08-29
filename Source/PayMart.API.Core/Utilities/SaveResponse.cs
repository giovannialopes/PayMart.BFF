namespace PayMart.API.Core.Utilities;

public class SaveResponse
{
    private static string _tokenId = "";
    public static void SaveUserToken(string tokenId) => _tokenId = tokenId;
    public static string GetUserToken() => _tokenId;


    private static int _orderId;
    public static void SaveOrderId(int orderId) => _orderId = orderId;
    public static int GetOrderId() => _orderId;


    private static string _price = "";
    public static void SavePrice(string price) => _price = price;
    public static string GetPrice() => _price;


}
