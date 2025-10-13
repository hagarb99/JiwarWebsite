using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
   
        public partial class Subscription : BaseModel
    {

            public int UserID { get; set; }
            public string PlanType { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Status { get; set; }

            public static bool IsValidPlanType(string planType)
            {
                return Enum.TryParse<PlanTypeEnum>(planType, true, out var result) &&
                       Enum.IsDefined(typeof(PlanTypeEnum), result);
            }

            public static bool IsValidStatus(string status)
            {
                return Enum.TryParse<StatusEnum>(status, true, out var result) &&
                       Enum.IsDefined(typeof(StatusEnum), result);
            }
        }
    }




