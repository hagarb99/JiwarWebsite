using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public class InteriorDesigner : BaseModel
    {
        public string DesignerID { get; set; }
        public string? Specialization { get; set; }
        public int? ExperienceYears { get; set; }
        public string? PortfolioURL { get; set; }

        // other offer fields (e.g., Title, Price, etc.)

        // Navigation property
        public virtual User User { get; set; }
        public virtual ICollection<Design> Designs { get; set; } = new List<Design>();


    }
}
