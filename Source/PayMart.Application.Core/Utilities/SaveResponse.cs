using System.Net.Http;

namespace PayMart.Application.Core.Utilities;

public class SaveResponse
{
    private static int _userId;
    public static void SaveUserId(int userId) => _userId = userId;
    public static int GetUserId() => _userId;


    private static int _orderId;
    public static void SaveOrderId(int orderId) => _orderId = orderId;
    public static int GetOrderId() => _orderId;


    private static string _price;
    public static void SavePrice(string price) => _price = price;
    public static string GetPrice() => _price;

}
