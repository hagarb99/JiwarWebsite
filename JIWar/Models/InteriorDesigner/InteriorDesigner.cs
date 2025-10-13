using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public class InteriorDesigner : BaseModel
    {
        public string Specialization { get; set; }
        public int? ExperienceYears { get; set; }
        public string PortfolioURL { get; set; }
    }
}
