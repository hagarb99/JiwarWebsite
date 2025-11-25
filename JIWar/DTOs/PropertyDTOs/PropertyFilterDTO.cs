using GEWAR.Models;

namespace Jiwar.DTOs.PropertyDTOs
{
    public class PropertyFilterDTO
    {
        public string? District { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinArea { get; set; }
        public decimal? MaxArea { get; set; }
        public int? NumBedrooms { get; set; }
        public int? NumBathrooms { get; set; }
        public PropertyType? PropertyType { get; set; } 
    }

}

