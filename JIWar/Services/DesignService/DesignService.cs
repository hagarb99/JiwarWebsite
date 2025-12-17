using AutoMapper;
using GEWAR;
using GEWAR.Models;
using Google;
using Jiwar.DTOs.DesignDto;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Jiwar.Services.DesignService
{
    public class DesignService : IDesignService
    {
        private readonly GiwarContext _context;
        private readonly IMapper _mapper;

        public DesignService(GiwarContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DesignDto> UploadFinalDesignAsync(string designerId, CreateDesignDto dto)
        {
            var proposal = await _context.DesignerProposals
                .Include(p => p.DesignRequest)
                .FirstOrDefaultAsync(p => p.Id == dto.ProposalID);

            if (proposal == null)
                throw new Exception("Proposal not found");

            var design = new Design
            {
                DesignerID = designerId,
                OwnerID = proposal.DesignRequest.UserID,
                PropertyID = dto.PropertyID,
                ProposalID = dto.ProposalID,
                ImageURLs = dto.ImageURLs ?? new List<string>(),
                AI_Generated = dto.AI_Generated,
                SelectedStyle = dto.SelectedStyle,
                Description = dto.Description,
                CreationDate = DateTime.UtcNow
            };

            _context.Designs.Add(design);

            proposal.DesignRequest.Status = "Completed";

            await _context.SaveChangesAsync();

            return _mapper.Map<DesignDto>(design);
        }

        public async Task<List<DesignDto>> GetDesignsByDesignerAsync(string designerId)
        {
            var designs = await _context.Designs
                .Where(d => d.DesignerID == designerId)
                .ToListAsync();

            return _mapper.Map<List<DesignDto>>(designs);
        }

        public async Task<List<DesignDto>> GetDesignsByOwnerAsync(string ownerId)
        {
            var designs = await _context.Designs
                .Where(d => d.OwnerID == ownerId)
                .ToListAsync();

            return _mapper.Map<List<DesignDto>>(designs);
        }

        public async Task<List<DesignDto>> GetDesignsByPropertyAsync(int propertyId)
        {
            var designs = await _context.Designs
                .Where(d => d.PropertyID == propertyId)
                .ToListAsync();

            return _mapper.Map<List<DesignDto>>(designs);
        }

        public async Task<DesignDto> GetDesignByIdAsync(int id)
        {
            var design = await _context.Designs.FindAsync(id);
            return _mapper.Map<DesignDto>(design);
        }
    }

}
