using Jiwar.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace GEWAR.Models
    {
        public partial class Notification : BaseModel
        {
            public int NotificationID { get; set; }
            public int UserID { get; set; }             
            public string Title { get; set; }             
            public string Message { get; set; }           
            public NotificationType NotificationType { get; set; } 
            public bool IsRead { get; set; } = false;
            public DateTime SentDate { get; set; }
            public virtual User User { get; set; } = null!;
        }
    }


