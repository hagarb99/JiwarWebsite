using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public class Design : BaseModel
    {
        public string DesignerID { get; set; }       // FK → InteriorDesigner.DesignerID
        public string DesignURL { get; set; }     // رابط التصميم (قد يكون صورة أو ملف)
        public bool AI_Generated { get; set; }    // هل التصميم تم إنشاؤه بالذكاء الاصطناعي؟
        public string? SelectedStyle { get; set; } // نوع الديكور أو النمط المختار
        public DateTime CreationDate { get; set; }

        // 🔗 Navigation Properties
        public virtual Request?  Request { get; set; }
        public virtual InteriorDesigner? InteriorDesigner { get; set; }
        public int RequestID { get; internal set; }
    }
}
    
