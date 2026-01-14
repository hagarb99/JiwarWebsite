using GEWAR.Models;
using Jiwar.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jiwar.Models.CustomerPropertyChat
{
    public class CustomerPropertyMessage : BaseModel
    {
        public string SenderId { get; set; }
        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }

        public string ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public virtual User Receiver { get; set; }

        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }

        public string MessageText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
    }
}
