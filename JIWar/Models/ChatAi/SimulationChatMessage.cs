using GEWAR.Models;
using Jiwar.Services.AI.Enums;

namespace Jiwar.Models.ChatAi
{
    public class SimulationChatMessage
    {
        public int Id { get; set; }

        public int RenovationSimulationID { get; set; }
        public virtual RenovationSimulation RenovationSimulation { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public ChatSenderEnum Sender { get; set; } // User | AI
        public ChatMessageTypeEnum MessageType { get; set; } // Text, Image, Voice

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}