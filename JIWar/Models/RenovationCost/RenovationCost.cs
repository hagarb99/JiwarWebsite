using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public class RenovationCost : BaseModel
    {
        public int RenovationProjectID { get; set; } // FK → RenovationProject

        public string CostType { get; set; }
        public decimal? EstimatedValue { get; set; }

        // Navigation
        public virtual RenovationProject RenovationProject { get; set; }
    }

}

