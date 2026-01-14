using GEWAR.Models;
using Jiwar.Services;
using Microsoft.AspNetCore.Hosting;

public class ImgService : IImgService
{
    private readonly IWebHostEnvironment webHostEnvironment;

    public ImgService(IWebHostEnvironment webHostEnvironment)
    {
        this.webHostEnvironment = webHostEnvironment;
    }

    public async Task<List<PropertyMedia>> SavePropertyImagesAsync(
    int propertyId,
    List<IFormFile> images
)
    {
        var mediaList = new List<PropertyMedia>();

        var imagesFolderPath = Path.Combine(
            webHostEnvironment.WebRootPath,
            "images",
            "properties",
            propertyId.ToString()
        );

        Directory.CreateDirectory(imagesFolderPath);

        int order = 0;

        foreach (var image in images)
        {
            if (image.Length == 0) continue;

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var fullPath = Path.Combine(imagesFolderPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await image.CopyToAsync(stream);

            mediaList.Add(new PropertyMedia
            {
                PropertyID = propertyId,
                MediaURL = $"/images/properties/{propertyId}/{fileName}",
                Order = order++,
                MediaType = "image",
                mediaTypeEnum = MediaTypeEnum.Image
            });
        }

        return mediaList;
    }

    public async Task<string> SaveUserProfileImageAsync(string userId, IFormFile image)
    {
        if (image == null || image.Length == 0) return null;

        // تنظيم الصور في فولدر خاص بالبروفايلات
        var folderPath = Path.Combine(webHostEnvironment.WebRootPath, "images", "profiles");
        Directory.CreateDirectory(folderPath);

        var fileName = $"{userId}_{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        var fullPath = Path.Combine(folderPath, fileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await image.CopyToAsync(stream);

        // نرجع المسار الذي سيخزن في الداتابيز
        return $"/images/profiles/{fileName}";
    }

    public async Task<string> SaveChatFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0) return null;

        var folderPath = Path.Combine(webHostEnvironment.WebRootPath, "uploads", "chat");
        Directory.CreateDirectory(folderPath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fullPath = Path.Combine(folderPath, fileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/uploads/chat/{fileName}";
    }

}