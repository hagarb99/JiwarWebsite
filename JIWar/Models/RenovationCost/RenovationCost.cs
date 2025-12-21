using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public class RenovationCost : BaseModel
    {
        public int RenovationProjectID { get; set; }  

        public string CostType { get; set; }
        public decimal? EstimatedValue { get; set; }

        public virtual RenovationProject RenovationProject { get; set; } = null!;
    }

}

