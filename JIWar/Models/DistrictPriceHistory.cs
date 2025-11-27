using GEWAR.Models;

namespace Jiwar.Models
{
    public class DistrictPriceHistory : BaseModel
    {
        public int Id { get; set; }

        public string District { get; set; } = string.Empty;

        public decimal AvgPricePerMeter { get; set; }

        public DateTime RecordDate { get; set; } = DateTime.UtcNow;
    }
}
