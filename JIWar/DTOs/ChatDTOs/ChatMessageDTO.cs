using Jiwar.Enum;

namespace Jiwar.DTOs.ChatDTOs
{
    public class ChatMessageDTO
    {
        public int? PropertyID { get; set; }  
        public string? SenderID { get; set; }
        public string? ReceiverID { get; set; }
        public string? SenderName { get; set; }
        public string? SenderPhoto { get; set; }
        public string? Message { get; set; } // Matches "message" in JSON
        public string? MessageText { get; set; } // For backward compatibility
        public MessageType MessageType { get; set; } = MessageType.Text;
        public DateTime SentDate { get; set; }
    }
}
