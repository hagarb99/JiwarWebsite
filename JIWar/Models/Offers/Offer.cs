using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public class Offer : BaseModel
    {
        public int OfferID { get; set; }
        public string BuyerID { get; set; }
        public int PropertyID { get; set; }
        public decimal OfferAmount { get; set; }
        public StatusEnum status { get; set; }
        public DateTime OfferDate { get; set; } = DateTime.Now;
        public virtual User Buyer { get; set; }
        public virtual Property Property { get; set; }


        // other offer fields (e.g., Title, Price, etc.)

        // Navigation property
    }
}
