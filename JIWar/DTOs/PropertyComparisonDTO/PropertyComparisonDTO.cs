using Jiwar.Enum;

namespace Jiwar.DTOs.PropertyComparisonDTO
{
    public class PropertyComparisonDTO
    {
        public int PropertyID { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public decimal? Area_sqm { get; set; }
        public int? NumBedrooms { get; set; }
        public int? NumBathrooms { get; set; }
        public string PropertyType { get; set; }
        public PropEnum Status { get; set; }
        public string ThumbnailUrl { get; set; }
        public List<string> Features { get; set; }


    }
}
