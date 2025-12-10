using Jiwar.Models;
using JIWAR.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public partial class User : IdentityUser
    {
        public string Name { get; set; }
        public string ProfilePicURL { get; set; }
        public DateTime RegistrationDate { get; set; }
        [NotMapped]
        public string SentMessages { get; internal set; }
        public string Role { get; set; }
        public virtual PropertyOwner propertyOwner { get; set; }

        public string GoogleId { get; set; }


        public virtual ICollection<InvestmentPortfolio> InvestmentPortfolios { get; set; } = new List<InvestmentPortfolio>();
        public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public virtual InteriorDesigner? InteriorDesigner { get; set; }
        public virtual ICollection<BookingRating> BookingRatings { get; set; } = new List<BookingRating>();
        public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
        public virtual ICollection<WishList> WishLists { get; set; } = new List<WishList>();

    }
}
