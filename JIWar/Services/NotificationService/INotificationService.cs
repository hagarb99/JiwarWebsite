using Jiwar.DTOs.NotificationDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jiwar.Services.NotificationService
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(string userId);
        Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync(string userId);
    }
}
