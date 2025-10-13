using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace GEWAR.Models
    {
        public partial class Notification : BaseModel
        {
            public int UserID { get; set; }                // FK → User.UserID (receiver)
            public string Title { get; set; }              // Short message title
            public string Message { get; set; }            // Notification content
            public NotificationType NotificationType { get; set; }  // Enum type
            public bool IsRead { get; set; } = false;      // Has the user opened it?
            public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // When sent

            // 🔗 Navigation Property
            public virtual User User { get; set; } = null!;
        }

        // 🧩 Enum for NotificationType
        public enum NotificationType
        {
            Booking,
            Offer,
            Request,
            System,
            Chat
        }
    }


