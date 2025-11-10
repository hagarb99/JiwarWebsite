using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public string OfferStatus { get; set; } = "Pending";
        public DateTime OfferDate { get; set; } = DateTime.Now;
        public User Buyer { get; set; }
        public Propertie Propertie { get; set; }

        public string UserID { get; set; }   // FK → User.Id

        // other offer fields (e.g., Title, Price, etc.)

        // Navigation property
        public virtual User User { get; set; }
    }
}
