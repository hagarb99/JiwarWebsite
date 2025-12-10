using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public partial class Request : BaseModel
    {

        public string CustomerID { get; set; }
        public int PropertyID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public StatusEnumReqPro StatusEnumRequest { get; set; }
        public virtual Property property { get; set; }

        public virtual ICollection<RequestRating> Ratings { get; set; } = new List<RequestRating>();
        public virtual ICollection<Proposal> Proposals { get; set; } = new List<Proposal>();  // one to many proposals

        // علاقات أخرى لو عندك
        public virtual User User { get; set; }
        //public virtual InteriorDesigner Designer { get; set; }
        public virtual ICollection<Design> Designs { get; set; } = new List<Design>();

    }
}
