using GEWAR.Models;

namespace Jiwar.DTOs.BookingDTOs
{
    public class CreateBookingDto
    {
        public int PropertyID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? OfferID { get; set; } // optional
        public PaymentMethod PaymentMethod { get; set; }
    }

}
