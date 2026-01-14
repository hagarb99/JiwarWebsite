using Jiwar.Enum;

namespace Jiwar.DTOs.ChatDTOs
{
    public class ConversationDTO
    {
        public int PropertyID { get; set; }
        public int DesignRequestID { get; set; }
        public string OtherUserID { get; set; } = string.Empty;
        public string OtherUserName { get; set; } = string.Empty;
        public string OtherUserPhoto { get; set; } = string.Empty;
        public string LastMessage { get; set; } = string.Empty;
        public MessageType LastMessageType { get; set; }
        public DateTime LastMessageDate { get; set; }
        public int UnreadCount { get; set; }
    }
}
