namespace Jiwar.DTOs
{
    public class PaymobWebhookDto
    {
        public PaymobObjectDto Obj { get; set; } = default!;
        public bool Success { get; set; }
        public bool Pending { get; set; }
        public bool IsRefunded { get; set; }
        public string Hmac { get; set; } = string.Empty;
    }
}
