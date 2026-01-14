using System;

namespace Jiwar.DTOs.CustomerPropertyChat
{
    public class SendMessageDto
    {
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public int PropertyId { get; set; }
        public string MessageText { get; set; }
    }
}
