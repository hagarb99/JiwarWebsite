using GEWAR.Models;

namespace Jiwar.Models
{
    public class PropertyPriceHistory : BaseModel
    {
        public int PropertyPriceHistoryId { get; set; }   

        public int PropertyID { get; set; }               
        public virtual Property Property { get; set; }   

        public decimal Price { get; set; }              
        public DateTime DateRecorded { get; set; } = DateTime.UtcNow; 

    }
}
