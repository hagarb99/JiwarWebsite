namespace GEWAR.Models
{
    public partial class Booking
    {
        public enum PaymentStatusEnum
        {
            Pending,
            Completed,
            Failed,
            Refunded
        }
    }


}