using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{

         public class PropertyAnalytics : BaseModel
    {
        public int PropertyID { get; set; } // FK → Propertie.PropertyID

        public decimal? FairValue_Estimate { get; set; } // تقدير القيمة العادلة

        public string Price_Influence_Factors { get; set; } // JSON يحتوي العوامل المؤثرة في السعر

        public DateTime AnalysisDate { get; set; } = DateTime.UtcNow; // تاريخ التحليل

        // Navigation property
        public virtual Propertie Property { get; set; }
    }
    }
