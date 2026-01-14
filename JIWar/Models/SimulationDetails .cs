using GEWAR.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Jiwar.Models
{
    public class SimulationDetails : BaseModel
    {
        public int RenovationSimulationID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Size { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public string Condition { get; set; } = string.Empty;
        public int? YearBuilt { get; set; }

        public virtual RenovationSimulation RenovationSimulation { get; set; } = null!;
    }
}