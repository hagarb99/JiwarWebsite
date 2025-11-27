namespace Jiwar.DTOs
{
    public class BuyBookingDto
    {
        public string UserId { get; set; }
        public int BookingId { get; set; }
        public PaymentMethod Method { get; set; }

    }
}
