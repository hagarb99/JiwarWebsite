using GEWAR.Models;

namespace GEWAR.Models
{
    public class Report : BaseModel
{
    public string UserID { get; set; }
    public int? BookingID { get; set; }

    public string ReportType { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual User User { get; set; }
    public virtual Booking? Booking { get; set; }
}
}

