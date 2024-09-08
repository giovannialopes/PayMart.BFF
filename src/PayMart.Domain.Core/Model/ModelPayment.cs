namespace PayMart.Domain.Core.Model;

public class ModelPayment
{
    public class PaymentRequest
    {
        public int PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
    }

    public class PaymentResponse
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal AmountPaid { get; set; }
        public string Protocol { get; set; } = string.Empty;
    }
}
