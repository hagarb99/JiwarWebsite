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
        public int UserID { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        public User User { get; set; }
    }
}
