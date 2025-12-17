using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
   
        public partial class Subscription : BaseModel
    {
     
            public string UserID { get; set; }
            public string PlanType { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Status { get; set; }
        public virtual User User { get; set; }

        public PlanTypeEnum planTypeEnum { get; set; }

        public StatusEnum2 statusEnum2 { get; set; }
        public string Name { get; internal set; }
        public decimal Price { get; internal set; }
        public int DurationInMonths { get; internal set; }
    }
    }




