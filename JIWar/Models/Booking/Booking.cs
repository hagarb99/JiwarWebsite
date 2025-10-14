using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public partial class Booking : BaseModel
    {

      
        public int PropertyID { get; set; }
        public int CustomerID { get; set; }
        public int? OfferID { get; set; }
        public string Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string PaymentStatus { get; set; }


        public virtual User Customer { get; set; }           // العميل اللي عمل الحجز
        public virtual Property Property { get; set; }       // العقار المحجوز
        public virtual Offer Offer { get; set; }             // العرض المرتبط (اختياري)
        public virtual BookingRating BookingRating { get; set; } // تقييم الحجز (1:1)





        //public static bool IsValidStatus(string status)
        //{
        //    return Enum.TryParse<StatusEnum>(status, true, out var result) &&
        //           Enum.IsDefined(typeof(StatusEnum), result);
        //}

        //public static bool IsValidPaymentStatus(string paymentStatus)
        //{
        //    return Enum.TryParse<PaymentStatusEnum>(paymentStatus, true, out var result) &&
        //           Enum.IsDefined(typeof(PaymentStatusEnum), result);
        //}
    }


}