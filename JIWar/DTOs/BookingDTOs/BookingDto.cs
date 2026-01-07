using GEWAR.Models;

namespace Jiwar.DTOs.BookingDTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int PropertyID { get; set; }
        public string CustomerID { get; set; }
        public int? OfferID { get; set; }
        public PaymentStatusEnum PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Cost { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public StatusEnum Status { get; set; }
    }

}
