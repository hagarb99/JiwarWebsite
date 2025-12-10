using System;
using GEWAR;
using GEWAR.Models;
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

        public async Task<Proposal> CreateProposalAsync(Proposal proposal)
        {
            await _context.Proposals.AddAsync(proposal);
            await _context.SaveChangesAsync();
            return proposal;
        }

        public async Task<IEnumerable<Proposal>> GetProposalsByDesignerAsync(string designerId)
        {
            return await _context.Proposals
                .Where(p => p.DesignerID == designerId)
                .ToListAsync();
        }

        public async Task UpdateProposalStatusAsync(int proposalId, string status)
        {
            var proposal = await _context.Proposals.FindAsync(proposalId);
            proposal.Status = status;
            await _context.SaveChangesAsync();
        }
    }

}
