using System;

namespace Jiwar.DTOs.CustomerPropertyChat
{
    public class CustomerMessageDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderProfilePicURL { get; set; }
        public string ReceiverId { get; set; }
        public int PropertyId { get; set; }
        public string MessageText { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public bool IsMine { get; set; } 
    }
}
