using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GEWAR.Models
{
    public partial class PropertyMedia : BaseModel
    {

        public int PropertyID { get; set; }
        public string MediaURL { get; set; }
        public string MediaType { get; set; }
        public DateTime UploadedDate { get; set; }
        public virtual Property Property { get; set; } = null!;
        public MediaTypeEnum mediaTypeEnum { get; set; }
    }
}
