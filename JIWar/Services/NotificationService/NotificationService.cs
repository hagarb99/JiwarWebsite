using Jiwar.DTOs.NotificationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEWAR;
using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using Jiwar.Enum;

namespace Jiwar.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly GiwarContext _context;

        public NotificationService(GiwarContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(string userId)
        {
            var notifications = await _context.Notifications
                                 .Where(n => n.UserID == userId)
                                 .OrderByDescending(n => n.SentDate)
                                 .ToListAsync();

            // Auto-mark all unread notifications as read when user views them
            var unreadNotifications = notifications.Where(n => !n.IsRead).ToList();
            if (unreadNotifications.Any())
            {
                foreach (var notification in unreadNotifications)
                {
                    notification.IsRead = true;
                }
                await _context.SaveChangesAsync();
            }

            return notifications.Select(n => new NotificationDto
            {
                NotificationID = n.NotificationID,
                Title = n.Title,
                Message = n.Message,
                NotificationType = n.NotificationType.ToString(),
                IsRead = true, // Always return true since we just marked them as read
                SentDate = n.SentDate,
                TimeAgo = GetTimeAgo(n.SentDate),
                RelatedId = n.RelatedId
            });
        }

        private string GetTimeAgo(DateTime date)
        {
            var span = DateTime.Now - date;
            if (span.TotalMinutes < 1) return "Just now";
            if (span.TotalMinutes < 60) return $"{(int)span.TotalMinutes}m ago";
            if (span.TotalHours < 24) return $"{(int)span.TotalHours}h ago";
            return $"{(int)span.TotalDays}d ago";
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAllAsReadAsync(string userId)
        {
            var notifications = await _context.Notifications
                                              .Where(n => n.UserID == userId && !n.IsRead)
                                              .ToListAsync();
            
            if (notifications.Any())
            {
                foreach (var n in notifications)
                {
                    n.IsRead = true;
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateNotificationAsync(string userId, string title, string message, string relatedId, string type)
        {
            if (!System.Enum.TryParse(type, true, out NotificationType notType))
            {
                notType = NotificationType.System;
            }

            var notification = new Notification
            {
                UserID = userId,
                Title = title,
                Message = message,
                RelatedId = relatedId,
                NotificationType = notType,
                SentDate = DateTime.Now,
                IsRead = false
            };

            // Ensure User exists to avoid foreign key errors? 
            // Usually we assume userId is valid as it comes from Identity.
            
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }
    }
}
