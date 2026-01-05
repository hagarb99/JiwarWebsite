using GEWAR.Models;
using Jiwar.Models;

namespace Jiwar.Helpers
{
    public static class SimulationDetailsMapper
    {
        public static SimulationDetails FromProperty(Property property)
        {
            return new SimulationDetails
            {
                Size = (decimal)property.Area_sqm,
                Rooms = (int)property.NumBedrooms,
                Bathrooms = (int)property.NumBathrooms,
                Condition = property.Condition.ToString()
                // أي حقول تانية محتاجاها الـ AI
            };
        }
    }
}
