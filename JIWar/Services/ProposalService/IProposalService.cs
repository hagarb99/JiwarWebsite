using GEWAR.Models;

namespace Jiwar.Services.ProposalService
{
    public interface IProposalService
    {
        Task<Proposal> CreateProposalAsync(Proposal proposal);
        Task<IEnumerable<Proposal>> GetProposalsByDesignerAsync(string designerId);
        Task UpdateProposalStatusAsync(int proposalId, string status);
    }

}
