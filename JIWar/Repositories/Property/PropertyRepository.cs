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

        public async Task<IEnumerable<Property>> GetMyPropertiesAsync(string ownerId)
{
                if (!int.TryParse(ownerId, out int ownerInt))
                return new List<Property>(); // أو throw exception حسب التصميم

          return await _dbSet
        .Where(p => p.OwnerID == ownerInt && p.IsDeleted == false)
        .Include(p => p.Images)
        .Include(p => p.Category)
        .ToListAsync();
}


        public async Task<Property> GetPropertyDetailsAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Images)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
        }

        public async Task AddPropertyMediaAsync(PropertyMedia media)
        {
            await _context.propertyMedias.AddAsync(media);
        }

        public async Task RemovePropertyMediaAsync(int mediaId)
        {
            var media = await _context.propertyMedias
                .FirstOrDefaultAsync(m => m.Id == mediaId);

            if (media != null)
            {
                media.IsDeleted = true;      // SOFT DELETE
                _context.propertyMedias.Update(media);
            }
        }

        public async Task UpdatePropertyStatusAsync(int id, PropEnum status)
        {
            var property = await _dbSet.FindAsync(id);

            if (property != null)
            {
                property.statusEnum = status;
                _dbSet.Update(property);
            }
        }
    }
}
