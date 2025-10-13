using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public class Propertie : BaseModel
    {
        public int PropertyID { get; set; }
        public int OwnerID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal? Area_sqm { get; set; }
        public int? NumBedrooms { get; set; }
        public int? NumBathrooms { get; set; }
        public string FinishingStatus { get; set; }
        public string PropertyType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
    }
}
