using Jiwar.DTOs.ReviewDTOs;
using System.Threading.Tasks;

namespace Jiwar.Services.ReviewService
{
    public interface IReviewService
    {
        Task<bool> SubmitReviewAsync(CreateReviewDto dto);
        Task<DesignerReviewSummaryDto> GetDesignerReviewsAsync(string designerId);
    }
}
