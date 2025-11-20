using GEWAR.Models;

namespace Jiwar.DTOs.BookingDTOs
{
    public class CreateBookingDto
    {
        public int PropertyID { get; set; }
        public int CustomerID { get; set; }
        public int? OfferID { get; set; }
        public StatusEnum Status { get; set; }
        public PaymentStatusEnum? PaymentStatus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Cost { get; set; }
    }

}
