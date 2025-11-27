using GEWAR.Models;

namespace Jiwar.Models
{
    public class BookingPayment : BaseModel
    {
        public string UserID { get; set; }
        public int BookingID { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatusEnum PaymentStatus { get; set; }
        public string PaymentReference { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual User User { get; set; }
        public virtual Booking Booking { get; set; }

    }
}
