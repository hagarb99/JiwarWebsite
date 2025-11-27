using GEWAR.Configurations;
using GEWAR.Models;
using GEWAR.Models.Configurations;
using Jiwar.Models;
using Jiwar.Models;
using Jiwar.Models.Offers;
using JIWAR.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace GEWAR
{

    /// <summary>
    /// SOMEEE SQL Server connection string
    /// /user id=hagarb_SQLLogin_1;pwd=zlvwboiwro
    /// </summary>
    public class GiwarContext : IdentityDbContext<User>
    {
        public GiwarContext(DbContextOptions<GiwarContext> options) : base(options)
        {

        }

        //tables
        public DbSet<User> Users { get; set; }
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
        public DbSet<PortfolioProperty> PortfolioProperties { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyAnalytics> PropertyAnalytics { get; set; }
        public DbSet<PropertyMedia> propertyMedias { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<RenovationCost> RenovationCosts { get; set; }
        public DbSet<RenovationProject> RenovationProjects { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Request> Requests { get; set; }

        public DbSet<RequestRating> RequestRatings { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<VirtualTour> VirtualTours { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<PropertyOwner> PropertyOwners { get; set; }

        public DbSet<PropertyFeature> PropertyFeatures{get;set;}

        public DbSet<Feature> Features{get ; set ;}
        public DbSet<ReportOrder> ReportOrders { get; set; }
        public DbSet<PropertyPriceHistory> PropertyPriceHistories { get; set; }

//<<<<<<< HEAD
        public DbSet<DistrictPriceHistory> DistrictPriceHistories { get; set; }
        public object DistrictPriceHistory { get; internal set; }
//=======
        public DbSet<BookingPayment> BookingPayments { get; set; }
//>>>>>>> 02b43b58d5d86a20d8f5436f5d65824c7904293d

        public DbSet<BookingPayment> BookingPayments { get; set; }
//>>>>>>> 02b43b58d5d86a20d8f5436f5d65824c7904293d



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("workstation id=JIWARDB.mssql.somee.com;packet size=4096;user id=hagarb_SQLLogin_1;pwd=zlvwboiwro;data source=JIWARDB.mssql.somee.com;persist security info=False;initial catalog=JIWARDB;TrustServerCertificate=True");
        //}

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
            modelBuilder.ApplyConfiguration(new PortfolioPropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyAnalyticsConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyMediaConfiguration());
            modelBuilder.ApplyConfiguration(new ProposalConfiguration());
            modelBuilder.ApplyConfiguration(new RenovationCostConfiguration());
            modelBuilder.ApplyConfiguration(new RenovationProjectConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new RequestConfiguration());
            modelBuilder.ApplyConfiguration(new RequestRatingConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new VirtualTourConfiguration());
            modelBuilder.ApplyConfiguration(new WishListConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyOwnerConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyFeatureConfiguration());
            modelBuilder.ApplyConfiguration(new ReportOrderConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyPriceHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new BookingPaymentConfiguration());



            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
