using AutoMapper;
using GEWAR;
using Google;
using Jiwar.DTOs.DesignDto;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;



namespace Jiwar.Services.DesignerProposalService
{
    public class DesignerProposalService : IDesignerProposalService
    {
        private readonly GiwarContext _context;
        private readonly IMapper _mapper;

        public DesignerProposalService(GiwarContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DesignerProposalDto> SendProposalAsync(string designerId, DesignerProposalDto dto)
        {
            var request = await _context.DesignRequests.FindAsync(dto.DesignRequestID);
            if (request == null)
                throw new Exception("Design request not found");

            if (request.Status == "InProgress" || request.Status == "Completed")
                throw new Exception("This request is no longer accepting proposals");

            var alreadySubmitted = await _context.DesignerProposals
                .AnyAsync(p => p.DesignRequestID == dto.DesignRequestID && p.DesignerID == designerId);

            if (alreadySubmitted)
                throw new Exception("You already submitted a proposal for this request");

            //var proposal = new DesignerProposal
            //{
            //    DesignRequestID = dto.DesignRequestID,
            //    DesignerID = designerId,
            //    EstimatedCost = dto.EstimatedCost,
            //    EstimatedDays = dto.EstimatedDays,
            //    ProposalDescription = dto.ProposalDescription,
            //    SampleDesignURL = dto.SampleDesignURL,
            //    Status = "Pending"
            //};
            var proposal = _mapper.Map<DesignerProposal>(dto);
            proposal.DesignerID = designerId;
            proposal.Status = "Pending";

            _context.DesignerProposals.Add(proposal);

            request.Status = "HasProposals";

            await _context.SaveChangesAsync();

            return _mapper.Map<DesignerProposalDto>(proposal);
        }

        public async Task<List<DesignerProposalDto>> GetProposalsForRequestAsync(int requestId)
        {
            var proposals = await _context.DesignerProposals
                .Where(p => p.DesignRequestID == requestId)
                .ToListAsync();

            return _mapper.Map<List<DesignerProposalDto>>(proposals);
        }

        public async Task<List<DesignerProposalDto>> GetProposalsForDesignerAsync(string designerId)
        {
            var proposals = await _context.DesignerProposals
                .Where(p => p.DesignerID == designerId)
                .ToListAsync();

            return _mapper.Map<List<DesignerProposalDto>>(proposals);
        }

        public async Task<DesignerProposalDto> ChooseProposalAsync(int proposalId, string ownerId)
        {
            var selected = await _context.DesignerProposals
                .Include(p => p.DesignRequest)
                .FirstOrDefaultAsync(p => p.Id == proposalId);

            if (selected == null)
                throw new Exception("Proposal not found");

            if (selected.DesignRequest.UserID != ownerId)
                throw new Exception("You are not the owner of this request");

            var allProposals = await _context.DesignerProposals
                .Where(p => p.DesignRequestID == selected.DesignRequestID)
                .ToListAsync();

            selected.Status = "Accepted";

            foreach (var p in allProposals)
            {
                if (p.Id != proposalId)
                    p.Status = "Rejected";
            }

            selected.DesignRequest.Status = "InProgress";

            await _context.SaveChangesAsync();

            return _mapper.Map<DesignerProposalDto>(selected);
        }
    }

}
