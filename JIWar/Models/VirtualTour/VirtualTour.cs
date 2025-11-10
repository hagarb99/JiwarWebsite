using Jiwar.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace GEWAR.Models
{
    public class VirtualTour : BaseModel
    {
        // 🔑 Foreign Keys
        public int PropertyID { get; set; }   // FK → Property.PropertyID
        public string UserID { get; set; }       // FK → User.UserID

        // 🧩 Attributes
        public string TourURL { get; set; }          // Link to video or 3D model
        public string TourTitle { get; set; }        // e.g., "Modern Villa 3D Tour"
        public string Description { get; set; }      // Optional text
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // 🔗 Navigation Properties
        public virtual Property Property { get; set; }
        public virtual User User { get; set; }
    }
}
