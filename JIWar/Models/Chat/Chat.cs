using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Jiwar.Enum.ChatTypeEnum;

namespace GEWAR.Models
{

    public class Chat : BaseModel
    {
        public int SenderID { get; set; }          // FK → User.UserID (sender)
        public int ReceiverID { get; set; }        // FK → User.UserID (receiver)
        public string MessageText { get; set; }    // Message content
        public MessageType MessageType { get; set; }  // Text, Image, or File
        public DateTime SentDate { get; set; } = DateTime.UtcNow;  // When sent

        // 🔗 Navigation Properties
        public virtual User Sender { get; set; } = null!;
        public virtual User Receiver { get; set; } = null!;
    }
}
