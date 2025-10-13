using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
   
         public partial class RequestRating : BaseModel
    {
        public int RequestID { get; set; }           // FK → Request.RequestID
        public int UserID { get; set; }              // FK → User.UserID (who rated)
        public int DesignerID { get; set; }          // FK → InteriorDesigner.DesignerID
        public int Rating { get; set; }              // Numeric score (1–5)
        public string Comment { get; set; }         // Optional feedback
        public DateTime CreatedDate { get; set; }    // When the rating was added

        // 🔗 Navigation Properties
        public virtual Request Request { get; set; }
        public virtual User User { get; set; }
        public virtual InteriorDesigner InteriorDesigner { get; set; }
    }
}

   