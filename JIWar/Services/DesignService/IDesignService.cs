using GEWAR.Models;

namespace Jiwar.Services.DesignService
{
    public interface IDesignService
    {
        Task<Design> UploadDesignAsync(Design design);
        Task<IEnumerable<Design>> GetDesignsByRequestAsync(int requestId);
    }

}
