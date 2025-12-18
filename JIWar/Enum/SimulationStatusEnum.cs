namespace GEWAR.Models
{
    namespace Jiwar.Enum
{
    public enum SimulationStatusEnum
    {
        Draft = 1,          // User started but didn't finish
        Submitted = 2,      // User completed inputs
        Analyzed = 3,       // AI generated recommendations
        ConvertedToProject = 4, // User turned it into RenovationProject
        Archived = 5
    }
}

}
