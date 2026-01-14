using Jiwar.DTOs.PropertyDTOs;
using JIWar.PropertyOwner;
using Jiwar.Enum;

namespace Jiwar.DTOs.AdminAnalytics
{
    public class AdminPropertyDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string OwnerName { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }
      

        public PropEnum status { get; set; }

        //public virtual PropertyComparisonDTO PropertyComparisonDTO { get; set; }

        public virtual PropertyDetailsDTO PropertyDetailsDTO { get; set; }


    }
}
