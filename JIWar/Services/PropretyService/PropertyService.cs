using AutoMapper;
using GEWAR;
using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.DTOs.PropertyDTOs;
using Jiwar.Enum;
using Jiwar.Models;
using Jiwar.Repositories;
using Jiwar.Repositories.Interfaces;
using Jiwar.Services;
using JIWar.PropertyOwner;
using Microsoft.EntityFrameworkCore;
namespace Jiwar.Service
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepo;
        private readonly IPropertyAnalyticsService propertyAnalyticsService;
        private readonly IImgService imgService;
        private readonly IMapper mapper;
        public PropertyService(
            IPropertyRepository propertyRepo,
            IPropertyAnalyticsService analyticsService,
            IMapper mapper,
            IImgService imgService
            )
        {
            _propertyRepo = propertyRepo;
            propertyAnalyticsService = analyticsService;
            this.mapper = mapper;
            this.imgService = imgService;
                }
        public async Task<PropertyWithAnalyticsDTO> AddPropertyAsync(
           PropertyCreateDTO dto,
           string ownerId
       )
        {
            var owner = await EnsureOwnerExistsAsync(ownerId);

            var property = new Property
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Address = dto.Address,
                City = dto.City,
                District = dto.District,
                Area_sqm = dto.Area,
                NumBedrooms = dto.Rooms,
                NumBathrooms = dto.Bathrooms,
                CategoryId = dto.CategoryId,
                Tour360Url = dto.Tour360Url,
                LocationLat = dto.LocationLat,
                LocationLang = dto.LocationLang,
                OwnerID = owner.UserID,
                ListingType = dto.ListingType,
                IsAvaliable = true
            };

            // 1️⃣ Save property first (to get PropertyID)
            await _propertyRepo.AddAsync(property);

            // 2️⃣ Save images using ImgService
            if (dto.Images != null && dto.Images.Any())
            {
                var mediaList = await imgService
                    .SavePropertyImagesAsync(property.PropertyID, dto.Images);

                foreach (var media in mediaList)
                {
                    await _propertyRepo.AddPropertyMediaAsync(media);
                }
            }

            // 3️⃣ Return response
            return new PropertyWithAnalyticsDTO
            {
                PropertyId = property.PropertyID,
                OwnerPrice = property.Price,
                Tour360Url = property.Tour360Url
            };
        }


        public async Task<PagedResult<PropertyListBDTO>> GetAllPropertiesAsync(int page, int pageSize)
        {
            return await _propertyRepo.GetAllPropertiesPagedAsync(page, pageSize);
        }


        // 2. Update Property
        public async Task<bool> UpdatePropertyAsync(PropertyUpdateDTO dto)
        {
            var existing = await _propertyRepo.GetByIdAsync(dto.Id);
            if (existing == null) return false;
            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            //_propertyRepo.Update(property);
            await _propertyRepo.UpdateAsync(existing);
            return true;
        }

        // 3. Delete Property (Soft Delete)
        public async Task<bool> DeletePropertyAsync(int id)
        {
            var property = await _propertyRepo.GetByIdAsync(id);
            if (property == null) return false;

            property.IsDeleted = true;
            //_propertyRepo.Update(property);
            await _propertyRepo.UpdateAsync(property);
            return true;
        }

        // 4. My Properties
        public async Task<IEnumerable<PropertyListBDTO>> GetMyPropertiesAsync(string ownerId)
        {
            var properties = await _propertyRepo.GetMyPropertiesAsync(ownerId);
            return mapper.Map<IEnumerable<PropertyListBDTO>>(properties);

        }

        public async Task AddPropertyMediaAsync(PropertyMedia media)
        {
            await _propertyRepo.AddPropertyMediaAsync(media);
        }

        public async Task RemovePropertyMediaAsync(int mediaId)
        {
            await _propertyRepo.RemovePropertyMediaAsync(mediaId);

        }

        public async Task UpdatePropertyStatusAsync(int id, PropEnum status)
        {
            await _propertyRepo.UpdatePropertyStatusAsync(id, status);

        }
        public async Task SendMessageAsync(Chat chat)
        {
            await _propertyRepo.SendMessageAsync(chat);
        }

        public async Task<IEnumerable<Chat>> GetChatHistoryAsync(string senderId, string receiverId, int propertyId)
        {
            return await _propertyRepo.GetChatHistoryAsync(senderId, receiverId, propertyId);
        }

        public async Task<IEnumerable<PropertyListBDTO>> GetFilteredPropertiesAsync(PropertyFilterDTO filter)
        {
            var properties = await _propertyRepo.GetFilteredPropertiesAsync(filter);
            return mapper.Map<IEnumerable<PropertyListBDTO>>(properties);

        }

        public async Task<IEnumerable<PropertyComparisonDTO>> GetPropertiesForComparisonAsync(List<int> propertyIds)
        {
            var properties = await _propertyRepo.GetPropertiesByIdsAsync(propertyIds);

            return properties.Select(p => new PropertyComparisonDTO
            {
                PropertyID = p.PropertyID,
                Title = p.Title,
                City = p.City,
                Address = p.Address,
                Price = p.Price,
                Area_sqm = p.Area_sqm,
                NumBedrooms = p.NumBedrooms,
                NumBathrooms = p.NumBathrooms,
                PropertyType = p.PropertyType.ToString(),
                Status = p.statusEnum,
                ThumbnailUrl = p.PropertyMedia.FirstOrDefault()?.MediaURL,
                Features = p.PropertyFeatures?
            .Where(pf => pf.Feature != null)
            .Select(pf => pf.Feature.Name)
            .ToList() ?? new List<string>()

            });

        }

        public async Task<PropertyOwner> EnsureOwnerExistsAsync(string ownerId)
        {
            var owner = await _propertyRepo.GetOwnerByIdAsync(ownerId);
            if (owner == null) owner = await _propertyRepo.CreateOwnerAsync(ownerId);
            return owner;
        }

        public async Task<PropertyDetailsDTO> GetPropertyDetailsDTOAsync(int id)
        {
            var property = await _propertyRepo.GetPropertyDetailsAsync(id);
            if (property == null || property.IsDeleted) return null;

            return mapper.Map<PropertyDetailsDTO>(property);
        }

    }
}
