using GEWAR.Models;
using Jiwar.Models;

namespace Jiwar.Services.ProposalService
{
    public interface IProposalService
    {
        Task<DesignerProposal> CreateProposalAsync(DesignerProposal proposal);
  
        Task<IEnumerable<DesignerProposal>> GetProposalsByDesignerAsync(string designerId);
        Task UpdateProposalStatusAsync(int proposalId, StatusEnumReqPro status);
    }

}
