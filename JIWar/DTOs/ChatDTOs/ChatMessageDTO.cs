using Jiwar.Enum;

namespace Jiwar.DTOs.ChatDTOs
{
    public class ChatMessageDTO
    {
        public int PropertyID { get; set; }  
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public string MessageText { get; set; }
        public MessageType MessageType { get; set; }
}
}
