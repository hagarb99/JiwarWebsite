using GEWAR;
using GEWAR.Models;
using Jiwar.DTOs.PropertyDTOs;
using Jiwar.Enum;
using Jiwar.Models;
using JIWar.PropertyOwner;
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

        public async Task<IEnumerable<Property>> GetMyPropertiesAsync(string ownerId)
{
          
            return await _dbSet
                .Where(p => p.OwnerID == ownerId && p.IsDeleted == false)
                .Include(p => p.PropertyMedia.Where(media => !media.IsDeleted))
                .Include(p => p.PriceHistory)
                .Include(p => p.PropertyOwner)
                .ToListAsync();
        }


        public async Task<Property> GetPropertyDetailsAsync(int id)
        {
            return await _dbSet
         .Include(p => p.PropertyMedia.Where(media => !media.IsDeleted))
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
                media.IsDeleted = true; 
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
                .Include(p => p.PropertyMedia.Where(m => !m.IsDeleted))
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
                .Include(p => p.PropertyMedia.Where(media => !media.IsDeleted))
               .Include(p => p.PropertyFeatures)
               .ThenInclude(pf => pf.Feature)
                .Where(p => ids.Contains(p.PropertyID) && !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<decimal> GetCityAveragePricePerSqmAsync(string city)
        {
            var properties = _context.Properties
        .Where(p => p.City == city && !p.IsDeleted);

            if (!await properties.AnyAsync())
                return 0m;

            var avgPricePerSqm = await properties
                .AverageAsync(p => (decimal?)(p.Price / p.Area_sqm));

            return avgPricePerSqm.GetValueOrDefault();
        }

        public async Task<List<Property>> GetComparablePropertiesAsync(
     string city,
     decimal price,
     string district,
     int areaTolerancePercentage,
     int ageToleranceYears,
     int minComps)
        {
            return await _context.Properties
                .Where(p => p.City == city &&
                            p.District == district &&
                            !p.IsDeleted)
                .Take(minComps)
                .ToListAsync();
        }

        Task<List<Property>> IPropertyRepository.GetComparablePropertiesAsync(string city, decimal price, string district, int areaTolerancePercentage, int ageToleranceYears, int minComps)
        {
            return GetComparablePropertiesAsync(city, price, district, areaTolerancePercentage, ageToleranceYears, minComps);
        }
        public async Task<PropertyOwner> GetOwnerByIdAsync(string ownerId)
        {
            return await _context.PropertyOwners.FirstOrDefaultAsync(o => o.UserID == ownerId);
        }

        public async Task<PropertyOwner> CreateOwnerAsync(string ownerId)
        {
            var owner = new PropertyOwner { UserID = ownerId };
            _context.PropertyOwners.Add(owner);
            await _context.SaveChangesAsync();
            return owner;
        }


        public async Task UpdateAsync(Property property)
        {
            _dbSet.Update(property);
            await _context.SaveChangesAsync();
        }

        //public async Task<PagedResult<PropertyListBDTO>> GetAllPropertiesPagedAsync(int page, int pageSize)
        //{
        //    var query = _context.Properties
        //        .AsNoTracking()  // مهم للأداء
        //        .Where(p => !p.IsDeleted && p.IsAvaliable == true)
        //        .Include(p => p.PropertyMedia.Where(media => !media.IsDeleted))
        //        .OrderByDescending(p => p.PropertyID);  

        //    var totalCount = await query.CountAsync();

        //    var items = await query
        //        .Skip((page - 1) * pageSize)
        //        .Take(pageSize)
        //        .Select(static p => new PropertyListBDTO
        //        {
        //            PropertyID = p.PropertyID,
        //            Title = p.Title,
        //            Price = p.Price,
        //            City = p.City,
        //            District = p.District,
        //            Area_sqm = p.Area_sqm,
        //            NumBedrooms = p.NumBedrooms,
        //            NumBathrooms = p.NumBathrooms,
        //            //ThumbnailUrl = p.PropertyMedia
        //            //    .OrderBy(m => m.Order)
        //            //    .Select(m => m.MediaURL)
        //            //    .FirstOrDefault()



        //        })
        //        .ToListAsync();



        //    return new PagedResult<PropertyListBDTO>
        //    {
        //        Items = items,
        //        TotalCount = totalCount,
        //        Page = page,
        //        PageSize = pageSize
        //    };


        public async Task<PagedResult<PropertyListBDTO>> GetAllPropertiesPagedAsync(int page, int pageSize)
        {
            var query = _context.Properties
                .AsNoTracking()
                .Where(p => !p.IsDeleted && p.IsAvaliable == true)
                .Include(p => p.PropertyMedia
                    .Where(media => !media.IsDeleted)
                    .OrderBy(media => media.Order)
                    .Take(1))  
                .OrderByDescending(p => p.PropertyID)
                .Include(p => p.PropertyMedia.Where(m => !m.IsDeleted))
                .OrderByDescending(p => p.PropertyID);  

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PropertyListBDTO
                {
                    PropertyID = p.PropertyID,
                    Title = p.Title,
                    Price = p.Price,
                    City = p.City,
                    District = p.District,
                    Area_sqm = p.Area_sqm,
                    NumBedrooms = p.NumBedrooms,
                    NumBathrooms = p.NumBathrooms,
                    ThumbnailUrl = p.PropertyMedia
                        .FirstOrDefault() != null
                        ? p.PropertyMedia.FirstOrDefault().MediaURL
                        : null
                })
                .ToListAsync();

            return new PagedResult<PropertyListBDTO>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        
    }

    }
}
