using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GEWAR.Models
    {
        public partial class RenovationProject : BaseModel
    {

            public string UserID { get; set; }
            public int PropertyID { get; set; }
            public decimal? EstimatedCost { get; set; }
            public decimal? EstimatedProfit { get; set; }
            public string ProjectStatus { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        public virtual ICollection<RenovationCost> RenovationCosts { get; set; } = new List<RenovationCost>();

        public ProjectStatusEnum? ProjectStatusEnum { get; set; }
            //public static bool IsValidProjectStatus(string status)
            //{
            //    return Enum.TryParse<ProjectStatusEnum>(status, true, out var result) &&
            //           Enum.IsDefined(typeof(ProjectStatusEnum), result);
            //}
        }
    }


