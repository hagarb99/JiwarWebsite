using System;
using GEWAR;
using GEWAR.Models;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Services.ProposalService
{
    public class ProposalService : IProposalService
    {
        private readonly GiwarContext _context;

        public ProposalService(GiwarContext context)
        {
            _context = context;
        }

        public async Task<DesignerProposal> CreateProposalAsync(DesignerProposal proposal)
        {
            await _context.Proposals.AddAsync(proposal);
            await _context.SaveChangesAsync();
            return proposal;
        }

        public async Task<IEnumerable<DesignerProposal>> GetProposalsByDesignerAsync(string designerId)
        {
            return await _context.Proposals
                .Where(p => p.DesignerID == designerId)
                .ToListAsync();
        }

        public async Task UpdateProposalStatusAsync(int proposalId, StatusEnumReqPro status)
        {
            var proposal = await _context.Proposals.FindAsync(proposalId);
            proposal.StatusEnumReq = status;
            await _context.SaveChangesAsync();
        }
    }

}
