using GEWAR.Models;
using Jiwar.Controllers;
using Jiwar.Models;

namespace Jiwar.Services.RequestService
{
    public interface IRequestService
    {
        Task<IEnumerable<DesignRequest>> GetAvailableRequestsAsync();
        Task<DesignRequest> GetRequestByIdAsync(int requestId);
        Task UpdateStatusAsync(int requestId, string newStatus);
    }

}
