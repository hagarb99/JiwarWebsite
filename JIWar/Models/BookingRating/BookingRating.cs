using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{

    public class BookingRating : BaseModel
    {
        

        
        public int BookingID { get; set; } // FK → Booking.BookingID

    
        public int UserID { get; set; } // FK → User.UserID

      
        public int Rating { get; set; } // Rating score (1–5)

        
public string Comment { get; set; } // Optional comment

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // When rating was created

        //  Navigation properties
        public virtual Booking Booking { get; set; }
        public virtual User User { get; set; }
    }
}
