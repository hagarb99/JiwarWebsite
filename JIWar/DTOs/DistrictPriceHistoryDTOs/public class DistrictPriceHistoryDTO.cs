using Jiwar.DTOs.DistrictPricePointDTOs;

namespace Jiwar.DTOs.DistrictPriceHistoryDTOs
{
    public class DistrictPriceHistoryDTO
    {
        public List<DistrictPricePointDTO> Last1Year { get; set; }
        public List<DistrictPricePointDTO> Last3Years { get; set; }
        public List<DistrictPricePointDTO> Last5Years { get; set; }
        public string District { get; internal set; }
        public object History { get; internal set; }
    }

}
