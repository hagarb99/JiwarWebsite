using GEWAR;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;
  



namespace Jiwar.Repositories.Designs
{
    public class DesignRepository : GenericRepository<Design>
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
            return await _context.Designs.Where(d => d.Id == propertyId).ToListAsync(); 
        }
    
    
    }
}
