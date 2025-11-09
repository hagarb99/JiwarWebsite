using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GEWAR.Models
{
    public class PortfolioProperty : BaseModel
    {
        public int PortfolioPropertyID { get; set; }  // PK

        public int PortfolioID { get; set; }          // FK → InvestmentPortfolios
        public int PropertyID { get; set; }           // FK → Properties

        public decimal OriginalPurchasePrice { get; set; }
        public decimal CalculatedROI { get; set; }

        // Navigation properties 
        public virtual InvestmentPortfolio Portfolio { get; set; }
        public virtual Propertie Propertie { get; set; }
    }
}
