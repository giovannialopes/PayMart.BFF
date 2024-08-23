using System.Net.Http;

namespace PayMart.Application.Core.Utilities;

public class SaveResponse
{
    private static string _userId = "";
    public static void SaveUserToken(string userId) => _userId = userId;
    public static string GetUserToken() => _userId;


    private static int _orderId;
    public static void SaveOrderId(int orderId) => _orderId = orderId;
    public static int GetOrderId() => _orderId;


    private static string _price = "";
    public static void SavePrice(string price) => _price = price;
    public static string GetPrice() => _price;


}
