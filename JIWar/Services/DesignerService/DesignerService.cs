using System;
using GEWAR;
using GEWAR.Models;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Services.DesignerService
{
    public class DesignerService : IDesignerService
    {
        private readonly GiwarContext _context;

        public DesignerService(GiwarContext context)
        {
            _context = context;
        }

        public async Task<InteriorDesigner> GetDesignerProfileAsync(string designerId)
        {
            return await _context.InteriorDesigners
                .Include(d => d.Designs)
                .Include(d => d.Proposals)
                .FirstOrDefaultAsync(d => d.InteriorDesignerID == designerId);
        }

        public async Task UpdateDesignerProfileAsync(InteriorDesigner designer)
        {
            _context.InteriorDesigners.Update(designer);
            await _context.SaveChangesAsync();
        }
    }

}
