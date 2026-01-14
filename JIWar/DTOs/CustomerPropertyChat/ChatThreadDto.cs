using System;

namespace Jiwar.DTOs.CustomerPropertyChat
{
    public class ChatThreadDto
    {
        public int PropertyId { get; set; }
        public string PropertyTitle { get; set; }
        public string PropertyImage { get; set; }
        
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerImage { get; set; }
        
        public string LastMessage { get; set; }
        public DateTime LastMessageDate { get; set; }
        public int UnreadCount { get; set; }
    }
}
