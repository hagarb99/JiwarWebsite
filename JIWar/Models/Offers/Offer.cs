using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public  class Offer : BaseModel
    {
        
            public int BuyerID { get; set; }
            public int PropertyID { get; set; }
            public decimal OfferAmount { get; set; }
            public string OfferStatus { get; set; } = "Pending";
            public DateTime OfferDate { get; set; } = DateTime.Now;
        
    }
}
