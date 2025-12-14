using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public class InvestmentPortfolio : BaseModel
    {
        public int PortfolioID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<PortfolioProperty> PortfolioProperties { get; set; } = new List<PortfolioProperty>(); // Navigation property(table PortfolioPropertie)

    }
}
