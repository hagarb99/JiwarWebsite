using Jiwar.Enum;
using System;

namespace Jiwar.DTOs.NotificationDTOs
{
    public class NotificationDto
    {
        public int NotificationID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string NotificationType { get; set; } // Returning string is often easier for frontend
        public bool IsRead { get; set; }
        public DateTime SentDate { get; set; }
        public string TimeAgo { get; set; } // Helper for display (e.g. "2 mins ago")
    }
}
