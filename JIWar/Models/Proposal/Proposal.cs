using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GEWAR.Models
{
    public partial class Proposal : BaseModel
    {

        public int RequestID { get; set; }
        public string DesignerID { get; set; }
        public string OfferDetails { get; set; }
        public decimal? PriceEstimate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public StatusEnumReqPro StatusEnumReq { get; set; }
        public virtual Request Request { get; set; }
        public virtual InteriorDesigner Designer { get; set; }


    }
}