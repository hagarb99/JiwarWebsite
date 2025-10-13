using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public class WishList : BaseModel
    {
        public int UserID { get; set; }
        public int PropertyID { get; set; }
        public DateTime AddedDate { get; set; }
        public string Notes { get; set; }
    }
}
