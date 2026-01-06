using Jiwar.Enum;
using System;

namespace Jiwar.DTOs.NotificationDTOs
{
    public class NotificationDto
    {
        public int NotificationID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string NotificationType { get; set; }
        public bool IsRead { get; set; }
        public DateTime SentDate { get; set; }
        public string TimeAgo { get; set; }
        public string? RelatedId { get; set; }
    }
}
