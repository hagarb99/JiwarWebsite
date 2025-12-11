using GEWAR.Models;

namespace Jiwar.Services.RequestService
{
    public interface IRequestService
    {
        Task<IEnumerable<Request>> GetAvailableRequestsAsync();
        Task<Request> GetRequestByIdAsync(int requestId);
        Task UpdateStatusAsync(int requestId, string newStatus);
    }

}
