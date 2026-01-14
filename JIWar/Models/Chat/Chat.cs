using Jiwar.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{

    public class Chat : BaseModel
    {
        public string SenderID { get; set; } = null!;          // FK → User.UserID (sender)
        public string ReceiverID { get; set; } = null!;        // FK → User.UserID (receiver)
        public string MessageText { get; set; } = string.Empty;    // Message content
        public MessageType MessageType { get; set; }  // Text, Image, or File
        public DateTime SentDate { get; set; }  // When sent
        public bool IsRead { get; set; } = false;

        // 🔗 Navigation Properties
        public virtual User Sender { get; set; } = null!;
        public virtual User Receiver { get; set; } = null!;
        public int PropertyID { get; set; }
        public virtual Property Property { get; set; } = null!;
    }
}
