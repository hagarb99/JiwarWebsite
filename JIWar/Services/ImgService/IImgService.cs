using GEWAR.Models;

namespace Jiwar.Services;

public interface IImgService
{
    Task<List<PropertyMedia>> SavePropertyImagesAsync(
           int propertyId,
           List<IFormFile> images
       );
    Task<string> SaveChatFileAsync(IFormFile file);
}
