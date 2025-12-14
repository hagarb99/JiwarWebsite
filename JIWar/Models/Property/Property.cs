using Jiwar.Enum;
using Jiwar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public class Property : BaseModel
    {
        public int PropertyID { get; set; }
        public string OwnerID { get; set; } // FK → PropertyOwner
        public string Address { get; set; }
        
        public decimal? LocationLat { get; set; }
        public decimal? LocationLang { get; set; } //for google map
        public string? City { get; set; }
        public decimal? Area_sqm { get; set; }
        public int? NumBedrooms { get; set; }
        public int? NumBathrooms { get; set; }

        public string? Tour360Url { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool? IsAvaliable { get; set; }//booking or not

        //enum for status
        public PropEnum statusEnum { get; set; }
        public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();

        public virtual ICollection<PortfolioProperty> PortfolioProperties { get; set; } = new List<PortfolioProperty>();

        public virtual ICollection<PropertyAnalytics> PropertyAnalytics { get; set; } = new List<PropertyAnalytics>();

        public virtual ICollection<PropertyMedia> PropertyMedia { get; set; } = new List<PropertyMedia>();
        public virtual ICollection<WishList> WishLists { get; set; } = new List<WishList>();

        public virtual ICollection<PropertyFeature> PropertyFeatures { get; set; }
        public virtual ICollection<PropertyPriceHistory> PriceHistory { get; set; } = new List<PropertyPriceHistory>();


        public virtual User OwnerUser { get; set; }
        //make relation-many prop-prop one prop owner
        //
        public virtual PropertyOwner PropertyOwner { get; set; }
        public PropertyType PropertyType { get; set; }
        public string Title { get; internal set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal EstimatedPrice { get; set; }
        public string District { get; internal set; }
    }
}
