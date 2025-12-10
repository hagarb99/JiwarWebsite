using System;
using GEWAR;
using GEWAR.Models;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Services.DesignService
{
    public class DesignService : IDesignService
    {
        private readonly GiwarContext _context;

        public DesignService(GiwarContext context)
        {
            _context = context;
        }

        public async Task<Design> UploadDesignAsync(Design design)
        {
            await _context.Designs.AddAsync(design);
            await _context.SaveChangesAsync();
            return design;
        }

        public async Task<IEnumerable<Design>> GetDesignsByRequestAsync(int requestId)
        {
            return await _context.Designs
                .Where(d => d.RequestID == requestId)
                .ToListAsync();
        }
    }

}
