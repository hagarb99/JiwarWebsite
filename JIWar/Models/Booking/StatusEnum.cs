namespace GEWAR.Models
{
    public partial class Booking
    {
        public object BookingRating { get; internal set; }

        public enum StatusEnum
        {
            Pending,
            Confirmed,
            Cancelled
        }
    }


}