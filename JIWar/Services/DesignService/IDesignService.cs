using GEWAR.Models;
using Jiwar.DTOs.DesignDto;

namespace Jiwar.Services.DesignService
{
    public interface IDesignService
    {
        Task<DesignDto> UploadFinalDesignAsync(string designerId, CreateDesignDto dto);
        Task<List<DesignDto>> GetDesignsByDesignerAsync(string designerId);
        Task<List<DesignDto>> GetDesignsByOwnerAsync(string ownerId);
        Task<List<DesignDto>> GetDesignsByPropertyAsync(int propertyId);
        Task<DesignDto> GetDesignByIdAsync(int id);
    }

}
