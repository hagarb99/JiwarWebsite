using GEWAR.Models;

namespace Jiwar.DTOs
{
    public class CustomerBookingDto
    {
        public int Id { get; set; }
        public int PropertyID { get; set; }
        public string PropertyTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public StatusEnum Status { get; set; }

    }
}
