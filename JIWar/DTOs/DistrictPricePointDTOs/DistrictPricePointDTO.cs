namespace Jiwar.DTOs.DistrictPricePointDTOs
{
    public class DistrictPricePointDTO
    {
        public DateTime Date { get; set; }
        public decimal AvgPricePerMeter { get; set; }
        public object Year { get; internal set; }
        public object AvgPrice { get; internal set; }
    }
}
