using AutoMapper;
using GEWAR;
using Jiwar.DTOs.DesignDto;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Services.DesignRequestService
{
    public class DesignRequestService : IDesignRequestService
    {
        private readonly GiwarContext _context;
        private readonly IMapper _mapper;

        public DesignRequestService(GiwarContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DesignRequestDto> CreateDesignRequestAsync(string userId, DesignRequestDto dto)
        {
            // ممكن تضيفي هنا Validation على الـ Property وملكيته

            
            var request = _mapper.Map<DesignRequest>(dto);
            request.UserID = userId;
            request.Status = "Open";
            request.CreatedAt = DateTime.UtcNow;

            _context.DesignRequests.Add(request);
            await _context.SaveChangesAsync();

            // نحسب عدد العروض (في البداية صفر)
            var dtoResult = _mapper.Map<DesignRequestDto>(request);
            //dtoResult.ProposalCount = 0;

            return dtoResult;
        }

        public async Task<List<DesignRequestDto>> GetUserRequestsAsync(string userId)
        {
            var requests = await _context.DesignRequests
                .Include(r => r.Proposals)
                .Where(r => r.UserID == userId)
                .ToListAsync();

            var result = _mapper.Map<List<DesignRequestDto>>(requests);

            //foreach (var r in result)
            //{
            //    var original = requests.First(x => x.Id == r.Id);
            //    r.ProposalCount = original.Proposals?.Count ?? 0;
            //}

            return result;
        }

        public async Task<List<DesignRequestDto>> GetAvailableRequestsAsync()
        {
            var requests = await _context.DesignRequests
                .Include(r => r.Proposals)
                .Where(r => r.Status == "Open" || r.Status == "HasProposals")
                .ToListAsync();

            var result = _mapper.Map<List<DesignRequestDto>>(requests);

            //foreach (var r in result)
            //{
            //    var original = requests.First(x => x.Id == r.Id);
            //    r.ProposalCount = original.Proposals?.Count ?? 0;
            //}

            return result;
        }

        public async Task<DesignRequestDto> GetRequestByIdAsync(int id)
        {
            var request = await _context.DesignRequests
                .Include(r => r.Proposals)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null) return null;

            var dto = _mapper.Map<DesignRequestDto>(request);
            //dto.ProposalCount = request.Proposals?.Count ?? 0;
            return dto;
        }

        public async Task UpdateRequestStatusAsync(int requestId, string status)
        {
            var request = await _context.DesignRequests.FindAsync(requestId);
            if (request == null) return;

            request.Status = status;
            await _context.SaveChangesAsync();
        }
    }
}
