namespace Jiwar.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int PropertyID { get; set; }
        public int CustomerID { get; set; }
        public int? OfferID { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public decimal Cost { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

}
