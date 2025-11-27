using GEWAR.Models;
using JIWAR.Models;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiwar.Models
{
    public partial class Booking : BaseModel
    {

        public int PropertyID { get; set; }
        public string CustomerID { get; set; }
        public int? OfferID { get; set; }
        public StatusEnum status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatusEnum? PaymentStatus { get; set; }

        public decimal Cost { get; set; }
        public virtual User Customer { get; set; }           // العميل اللي عمل الحجز
        public virtual Property Property { get; set; }       // العقار المحجوز
        public virtual Offer Offer { get; set; }             // العرض المرتبط (اختياري)
        public virtual BookingRating BookingRating { get; set; } // تقييم الحجز (1:1)


    }


}