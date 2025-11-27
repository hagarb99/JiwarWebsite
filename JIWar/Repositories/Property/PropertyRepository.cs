using GEWAR;
using GEWAR.Models;
using Jiwar.DTOs.PropertyDTOs;
using Jiwar.Enum;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Repositories
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        private readonly GiwarContext _context;

        public PropertyRepository(GiwarContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Property>> GetMyPropertiesAsync(int ownerId)
{
          
            return await _dbSet
                .Where(p => p.OwnerID == ownerId && p.IsDeleted == false)
                .Include(p => p.PropertyMedia)
                .Include(p => p.PriceHistory)
                .Include(p => p.PropertyOwner)
                .ToListAsync();
        }


        public async Task<Property> GetPropertyDetailsAsync(int id)
        {
            return await _dbSet
         .Include(p => p.PropertyMedia)
         .Include(p => p.PriceHistory)
         .Include(p => p.PropertyOwner) 
         .FirstOrDefaultAsync(p => p.PropertyID == id && p.IsDeleted == false);

        }

        public async Task AddPropertyMediaAsync(PropertyMedia media)
        {
            await _context.Set<PropertyMedia>().AddAsync(media);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePropertyMediaAsync(int mediaId)
        {
            var media = await _context.Set<PropertyMedia>()
        .FirstOrDefaultAsync(m => m.Id == mediaId); 

            if (media != null)
            {
                media.IsDeleted = true; // Soft delete
                _context.Set<PropertyMedia>().Update(media);
                await _context.SaveChangesAsync();
            }

        }

        public async Task UpdatePropertyStatusAsync(int id, PropEnum status)
        {
            var property = await _dbSet.FirstOrDefaultAsync(p => p.PropertyID == id);

            if (property != null)
            {
                property.statusEnum = status;
                _dbSet.Update(property);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SendMessageAsync(Chat chat)
        {
            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Chat>> GetChatHistoryAsync(string senderId, string receiverId, int propertyId)
        {
            return await _context.Chats
                .Where(c => c.SenderID == senderId && c.ReceiverID == receiverId && c.PropertyID == propertyId
                         || c.SenderID == receiverId && c.ReceiverID == senderId && c.PropertyID == propertyId)
                .OrderBy(c => c.SentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetFilteredPropertiesAsync(PropertyFilterDTO filter)
        {
            var query = _context.Properties
                .Include(p => p.PropertyMedia)
                .Where(p => !p.IsDeleted);

            if (!string.IsNullOrEmpty(filter.District))
                query = query.Where(p => p.City == filter.District);

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            if (filter.MinArea.HasValue)
                query = query.Where(p => p.Area_sqm >= filter.MinArea.Value);

            if (filter.MaxArea.HasValue)
                query = query.Where(p => p.Area_sqm <= filter.MaxArea.Value);

            if (filter.NumBedrooms.HasValue)
                query = query.Where(p => p.NumBedrooms == filter.NumBedrooms.Value);

            if (filter.NumBathrooms.HasValue)
                query = query.Where(p => p.NumBathrooms == filter.NumBathrooms.Value);

            if (filter.PropertyType.HasValue)
                query = query.Where(p => p.PropertyType == filter.PropertyType.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetPropertiesByIdsAsync(List<int> ids)
        {
            return await _context.Properties
                .Include(p => p.PropertyMedia)
               .Include(p => p.PropertyFeatures)
               .ThenInclude(pf => pf.Feature)
                .Where(p => ids.Contains(p.PropertyID) && !p.IsDeleted)
                .ToListAsync();
        }

        public Task<decimal> GetCityAveragePricePerSqmAsync(string city)
        {
            throw new NotImplementedException();
        }

        public Task GetComparablePropertiesAsync(string city, decimal v1, string v2, int areaTolerancePercentage, int ageToleranceYears, int minComps)
        {
            throw new NotImplementedException();
        }
    }
}
