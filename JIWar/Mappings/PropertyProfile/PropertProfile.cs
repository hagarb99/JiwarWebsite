//using AutoMapper;
//using Jiwar.DTOs;
//using Jiwar.Models;
//using JIWar.PropertyOwner;

//namespace Jiwar.Mappings
//{
//    public class PropertyProfile : Profile
//    {
//        public PropertyProfile()
//        {
//            CreateMap<Property, PropertyListBDTO>()
//                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src =>
//                    src.PropertyMedia != null && src.PropertyMedia.Any(m => !m.IsDeleted)
//                    ? src.PropertyMedia.Where(m => !m.IsDeleted).OrderBy(m => m.Order).FirstOrDefault().MediaURL
//                    : null));

//            CreateMap<Property, PropertyDetailsDTO>()
//                .ForMember(dest => dest.MediaUrls, opt => opt.MapFrom(src =>
//                    src.PropertyMedia.Where(m => !m.IsDeleted).OrderBy(m => m.Order).Select(m => m.MediaURL).ToList()))
//                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src =>
//                    src.PropertyOwner != null && src.PropertyOwner.Owneruser != null
//                    ? src.PropertyOwner.Owneruser.Name
//                    : "Unknown"));
