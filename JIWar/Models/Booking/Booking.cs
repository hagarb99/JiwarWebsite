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