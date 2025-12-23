using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public class InteriorDesigner : BaseModel
    {
        public string? Specialization { get; set; }
        public int? ExperienceYears { get; set; }
        public string? PortfolioURL { get; set; }


        // Navigation property
        public virtual User User { get; set; }
        public string InteriorDesignerID { get; set; }
        //public virtual ICollection<Design> Designs { get; set; } = new List<Design>();
        public virtual ICollection<Proposal> Proposals { get; set; } = new List<Proposal>();
      
        ///property owner table-is a user
        //like his-subscription-


    }
}
