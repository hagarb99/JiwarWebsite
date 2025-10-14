namespace GEWAR.Models
{
    public class Complaint : BaseModel
    {
        public int UserID { get; set; }
        public int? DesignerID { get; set; }
        public int? BookingID { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual InteriorDesigner Designer { get; set; }
        public virtual Booking Booking { get; set; }
        public object ComplaintText { get; internal set; }
        public object Status { get; internal set; }
        public object CreatedDate { get; internal set; }
    }
}