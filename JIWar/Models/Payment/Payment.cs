using Jiwar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public class Payment : BaseModel
    {
        public int PaymentID { get; set; }
        public string UserID { get; set; }
        public string RelatedType { get; set; }
        public int RelatedID { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public PaymentStatusEnum Status { get; set; }

        public virtual User User { get; set; }
    }
}
