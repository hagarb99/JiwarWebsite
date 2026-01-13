using GEWAR;
using GEWAR.Models;
using Jiwar.DTOs.ReviewDTOs;
using Jiwar.Hubs;
using Jiwar.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using GEWAR.Models;
using Jiwar.Enum;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Jiwar.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly GiwarContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public ReviewService(GiwarContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<bool> SubmitReviewAsync(CreateReviewDto dto)
        {
            var alreadyReviewed = await _context.DesignerReviews
                .AnyAsync(r => r.ProposalID == dto.ProposalId);
            
            if (alreadyReviewed)
                throw new Exception("You have already reviewed this project.");

            var proposal = await _context.DesignerProposals
                .Include(p => p.DesignRequest)
                .FirstOrDefaultAsync(p => p.Id == dto.ProposalId);

            if (proposal == null || proposal.StatusEnumReq != StatusEnumReqPro.Delivered)
                throw new Exception("Proposal not found or project not yet delivered.");

            if (proposal.DesignRequest.UserID != dto.PropertyOwnerId)
                throw new Exception("Only the property owner can review this project.");

            var review = new DesignerReview
            {
                DesignerID = dto.DesignerId,
                PropertyOwnerID = dto.PropertyOwnerId,
                ProposalID = dto.ProposalId,
                DesignRequestID = dto.DesignRequestId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            _context.DesignerReviews.Add(review);
            await _context.SaveChangesAsync();

            // Update Designer Rating
            var designer = await _context.Users.FindAsync(dto.DesignerId);
            if (designer != null)
            {
                var reviews = await _context.DesignerReviews
                    .Where(r => r.DesignerID == dto.DesignerId)
                    .ToListAsync();

                designer.AverageRating = reviews.Average(r => r.Rating);
                designer.TotalReviews = reviews.Count;
                await _context.SaveChangesAsync();
            }

            // Notify Designer
            var notification = new Notification
            {
                UserID = dto.DesignerId,
                Title = "New Review Received ⭐",
                Message = $"You received a {dto.Rating}-star review from a client!",
                NotificationType = NotificationType.System,
                SentDate = DateTime.Now,
                IsRead = false
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.User(dto.DesignerId).SendAsync("ReceiveNotification", new
            {
                title = notification.Title,
                message = notification.Message,
                type = "Info",
                playSound = true
            });

            return true;
        }

        public async Task<DesignerReviewSummaryDto> GetDesignerReviewsAsync(string designerId)
        {
            var user = await _context.Users.FindAsync(designerId);
            var reviews = await _context.DesignerReviews
                .Include(r => r.PropertyOwner)
                .Where(r => r.DesignerID == designerId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return new DesignerReviewSummaryDto
            {
                DesignerId = designerId,
                AverageRating = user?.AverageRating ?? 0,
                TotalReviews = user?.TotalReviews ?? 0,
                Reviews = reviews.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    PropertyOwnerName = r.PropertyOwner.Name,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt
                }).ToList()
            };
        }
    }
}
