using System;
using GEWAR;
using GEWAR.Models;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Services.RequestService
{
    public class RequestService : IRequestService
    {
        private readonly GiwarContext _context;

        public RequestService(GiwarContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DesignRequest>> GetAvailableRequestsAsync()
        {
            return await _context.Requests
                .Where(r => r.Status == "Pending")
                .ToListAsync();
        }

        public async Task<DesignRequest> GetRequestByIdAsync(int requestId)
        {
            return await _context.Requests.FindAsync(requestId);
        }

        public async Task UpdateStatusAsync(int requestId, string newStatus)
        {
            var request = await _context.Requests.FindAsync(requestId);
            request.Status = newStatus;
            await _context.SaveChangesAsync();
        }
    }

}
