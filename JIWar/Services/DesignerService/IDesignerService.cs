using GEWAR.Models;

namespace Jiwar.Services.DesignerService
{
    public interface IDesignerService
    {
        Task<InteriorDesigner> GetDesignerProfileAsync(string designerId);
        Task UpdateDesignerProfileAsync(InteriorDesigner designer);
    }


}
