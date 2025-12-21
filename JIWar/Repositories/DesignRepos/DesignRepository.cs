using GEWAR;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;



namespace Jiwar.Repositories.Designs
{
    public class DesignRepository : GenericRepository<Design>, IDesignRepository
    { 
        private readonly GiwarContext _context;
        public DesignRepository(GiwarContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Design>> GetDesignsByDesignerAsync(string designerId)
        {
            return await _context.Designs.Where(d => d.DesignerID == designerId).ToListAsync();
        }
        public async Task<IEnumerable<Design>> GetDesignsByPropertyAsync(int propertyId)
        { 
            return await _context.Designs.Where(d => d.PropertyID == propertyId).ToListAsync(); 
        }
        public async Task<Design> GetDesignWithProposalAsync(int designId) 
        
        {
            return await _context.Designs.Include(d => d.Proposal).FirstOrDefaultAsync(d => d.Id == designId);
        
        }
    
    }
}
