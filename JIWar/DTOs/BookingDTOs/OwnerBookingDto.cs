using GEWAR.Models;

namespace Jiwar.DTOs
{
    public class OwnerBookingDto
    {
        public int Id { get; set; }
        public int PropertyID { get; set; }
        public string PropertyTitle { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Cost { get; set; }
        public StatusEnum Status { get; set; }
    }
}
