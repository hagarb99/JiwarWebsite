using Jiwar.Models;
namespace Jiwar.Repositories.Designs
{


   
        public interface IDesignRepository : IGenericRepository<Design>
        {
            Task<IEnumerable<Design>> GetDesignsByDesignerAsync(string designerId);
            Task<IEnumerable<Design>> GetDesignsByPropertyAsync(int propertyId);
            Task<Design> GetDesignWithProposalAsync(int designId);
        }
    

}
