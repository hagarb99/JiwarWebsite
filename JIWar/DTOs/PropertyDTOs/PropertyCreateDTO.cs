using GEWAR.Models;
using Jiwar.Enum;
using System.ComponentModel.DataAnnotations;

namespace JIWar.PropertyOwner
{
    
public class PropertyCreateDTO
{
        public string Title { get; set; }            // required
        public string Description { get; set; }      // required
        public decimal Price { get; set; }           // required
        public string Address { get; set; }          // required
        public string City { get; set; }             // required
        public int CategoryId { get; set; }          // required
        public string? Tour360Url { get; set; }      // optional
        public int? Rooms { get; set; }              // optional
        public int? Bathrooms { get; set; }          // optional
        public decimal? Area { get; set; }           // optional
        public decimal? LocationLat { get; set; }    // optional
        public decimal? LocationLang { get; set; }   // optional
        public string? District { get; set; }
        public List<IFormFile>? Images { get; set; }
        [Required]
        public PropertyTypeEnum? PropertyType { get; set; }
        public ListingTypeEnum ListingType { get; set; }
    }

}
