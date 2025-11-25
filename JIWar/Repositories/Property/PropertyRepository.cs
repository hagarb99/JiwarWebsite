using Microsoft.EntityFrameworkCore;
using Jiwar.Models;
using GEWAR.Models;
using Jiwar.Enum;
using GEWAR;

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
    }
}
