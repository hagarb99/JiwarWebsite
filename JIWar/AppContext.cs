using GEWAR.Configurations;
using GEWAR.Models;
using GEWAR.Models;
using GEWAR.Models.Configurations;
using Jiwar.Models;
using Jiwar.Models.Offers;
using JIWAR.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR
{

    /// <summary>
    /// SOMEEE SQL Server connection string
    /// /user id=hagarb_SQLLogin_1;pwd=zlvwboiwro
    /// </summary>
    public class AppContext : DbContext
    {
        //tables
        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingRating> BookingRating { get; set; }
        public DbSet<Chat> Chats { get; set; }

        public DbSet<Complaint> complaints { get; set; }
        public DbSet<Design> Designs { get; set; }
        public DbSet<InteriorDesigner> InteriorDesigners { get; set; }
        public DbSet<InvestmentPortfolio> InvestmentPortfolios { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PortfolioPropertie> PortfolioProperties { get; set; }
        public DbSet<Propertie> Properties { get; set; }
        public DbSet<PropertyAnalytic> PropertyAnalytics { get; set; }
        public DbSet<PropertyMedia> propertyMedias { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<RenovationCost> RenovationCosts { get; set; }
        public DbSet<RenovationProject> RenovationProjects { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Request> Requests { get; set; }

        public DbSet<RequestRating> RequestRatings { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VirtualTour> VirtualTours { get; set; }
        public DbSet<WishList> WishLists { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("workstation id=JIWARDB.mssql.somee.com;packet size=4096;user id=hagarb_SQLLogin_1;pwd=zlvwboiwro;data source=JIWARDB.mssql.somee.com;persist security info=False;initial catalog=JIWARDB;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new BookingRatingConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new ComplaintConfiguration());
            modelBuilder.ApplyConfiguration(new DesignConfiguration());
            modelBuilder.ApplyConfiguration(new InteriorDesignerConfiguration());
            modelBuilder.ApplyConfiguration(new InvestmentPortfolioConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new OfferConfiguration());
            modelBuilder.ApplyConfiguration( new PaymentConfiguration());
            //modelBuilder.ApplyConfiguration();
            //modelBuilder.ApplyConfiguration();
            //modelBuilder.ApplyConfiguration();
            //modelBuilder.ApplyConfiguration();
            //modelBuilder.ApplyConfiguration();
            modelBuilder.ApplyConfiguration(new RenovationCostConfiguration());
            modelBuilder.ApplyConfiguration(new RenovationProjectConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new RequestConfiguration());
            modelBuilder.ApplyConfiguration(new RequestRatingConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new VirtualTourConfiguration());
            modelBuilder.ApplyConfiguration(new WishListConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
