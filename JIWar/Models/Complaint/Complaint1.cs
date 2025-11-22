using Jiwar.Enum;
using Jiwar.Models;

namespace GEWAR.Models
{
    public class Complaint : BaseModel
    {
        public string UserID { get; set; }
        public string? DesignerID { get; set; }
        public int? BookingID { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual InteriorDesigner? Designer { get; set; }
        public virtual Booking? Booking { get; set; }
        public string ComplaintText { get; set; } = string.Empty;
        public ComplaintStatus Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    }
}