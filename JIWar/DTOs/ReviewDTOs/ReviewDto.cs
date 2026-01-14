using System;

namespace Jiwar.DTOs.ReviewDTOs
{
    public class CreateReviewDto
    {
        public int ProposalId { get; set; }
        public string DesignerId { get; set; }
        public string PropertyOwnerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int DesignRequestId { get; set; }
    }

    public class ReviewDto
    {
        public int Id { get; set; }
        public string PropertyOwnerName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class DesignerReviewSummaryDto
    {
        public string DesignerId { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }
}
