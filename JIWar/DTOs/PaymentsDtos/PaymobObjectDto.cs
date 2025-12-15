namespace Jiwar.DTOs
{
    public class PaymobObjectDto
    {
        public long OrderId { get; set; }
        public long Id { get; set; } // transaction_id
        public int AmountCents { get; set; }
        public string Currency { get; set; } = "EGP";
        public string MerchantOrderId { get; set; } = string.Empty;
    }
}
