using Jiwar.Models;
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
        public virtual ICollection<DesignerProposal> Proposals { get; set; } = new List<DesignerProposal>();
      


    }
}
