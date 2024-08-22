namespace PayMart.Domain.Core.Response.Payment;

public class ResponsePostPayment
{
    public string PaymentType { get; set; } = "";
    public DateTime DateTime { get; set; }
    public decimal Price { get; set; }
}
