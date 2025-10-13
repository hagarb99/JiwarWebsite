using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    internal class RenovationCost : BaseModel
    {
       
            public int ProjectID { get; set; }
            public string CostType { get; set; }
            public decimal? EstimatedValue { get; set; }
        }
    }

