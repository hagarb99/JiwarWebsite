using GEWAR.Models;

namespace Jiwar.Models
{
    public class Design : BaseModel
    {
        public string DesignerID { get; set; }
        public virtual InteriorDesigner InteriorDesigner { get; set; }

        public int? RequestID { get; set; }
        public virtual DesignRequest Request { get; set; }

        public List<string> ImageURLs { get; set; } = new();
        public bool AI_Generated { get; set; }
        public string SelectedStyle { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }

        public double Progress { get; set; }

    }

}
