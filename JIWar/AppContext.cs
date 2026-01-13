using GEWAR.Configurations;
using GEWAR.Data.Configurations;
using GEWAR.Models;
using GEWAR.Models.Configurations;
using Jiwar.Models;
using Jiwar.Models.ChatAi;
using Jiwar.Models.Offers;
using Jiwar.Models.Valuation;
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

        public DbSet<DesignRequest> DesignRequests { get; set; }
        public DbSet<DesignerProposal> DesignerProposals { get; set; }

        public DbSet<InteriorDesigner> InteriorDesigners { get; set; }
        public DbSet<InvestmentPortfolio> InvestmentPortfolios { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PortfolioProperty> PortfolioProperties { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyAnalytics> PropertyAnalytics { get; set; }
        public DbSet<PropertyMedia> propertyMedias { get; set; }
        public DbSet<DesignerProposal> Proposals { get; set; }
        public DbSet<RenovationCost> RenovationCosts { get; set; }
        public DbSet<RenovationProject> RenovationProjects { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<DesignRequest> Requests { get; set; }

        public DbSet<RequestRating> RequestRatings { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<VirtualTour> VirtualTours { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<PropertyOwner> PropertyOwners { get; set; }
        public DbSet<DesignerReview> DesignerReviews { get; set; }

        public DbSet<PropertyFeature> PropertyFeatures{get;set;}

        public DbSet<Feature> Features{get ; set ;}
        public DbSet<ReportOrder> ReportOrders { get; set; }
        public DbSet<PropertyPriceHistory> PropertyPriceHistories { get; set; }
        public DbSet<BookingPayment> BookingPayments { get; set; }
        public DbSet<ValuationHistory> ValuationHistories { get; set; }

        public DbSet<DistrictPriceHistory> DistrictPriceHistories { get; set; }
        //public DbSet<DistrictPriceHistory> DistrictPriceHistory { get;  set; }

        public DbSet<RenovationSimulation> RenovationSimulations { get; set; }
        public DbSet<SimulationRecommendation> SimulationRecommendations { get; set; }
        public DbSet<SimulationMedia> SimulationMedias { get; set; }

        public DbSet<SimulationDetails> SimulationDetails { get; set;}

        public DbSet<SimulationChatMessage> SimulationChatMessages { get; set; }

        public DbSet<UserChatQuota> UserChatQuotas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Property>(entity =>
            {
                entity.Property(e => e.EstimatedPrice).HasPrecision(18, 2);
                entity.Property(e => e.Price).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.Property(e => e.Price).HasPrecision(18, 2);
            });

            modelBuilder.Entity<DistrictPriceHistory>(entity =>
            {
                entity.Property(e => e.AvgPricePerMeter).HasPrecision(18, 2);
            });

            modelBuilder.Entity<ValuationHistory>(entity =>
            {
                entity.Property(e => e.Area).HasPrecision(18, 2);
                entity.Property(e => e.ConfidenceScore).HasPrecision(5, 2);
                entity.Property(e => e.MaxPrice).HasPrecision(18, 2);
                entity.Property(e => e.MinPrice).HasPrecision(18, 2);
                entity.Property(e => e.MostLikelyPrice).HasPrecision(18, 2);
            });

            // ثانياً: تطبيق جميع Configurations
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new BookingRatingConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new ComplaintConfiguration());
            modelBuilder.ApplyConfiguration(new DesignConfiguration());

            modelBuilder.ApplyConfiguration(new DesignRequestConfiguration());
            modelBuilder.ApplyConfiguration(new DesignerProposalConfiguration());
            // DesignConfiguration already applied

            modelBuilder.ApplyConfiguration(new InteriorDesignerConfiguration());
            modelBuilder.ApplyConfiguration(new InvestmentPortfolioConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new OfferConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new PortfolioPropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyAnalyticsConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyMediaConfiguration());
            modelBuilder.ApplyConfiguration(new DesignerProposalConfiguration());
            modelBuilder.ApplyConfiguration(new RenovationCostConfiguration());
            modelBuilder.ApplyConfiguration(new RenovationProjectConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new DesignRequestConfiguration());
            modelBuilder.ApplyConfiguration(new RequestRatingConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new VirtualTourConfiguration());
            modelBuilder.ApplyConfiguration(new WishListConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyOwnerConfiguration());
            modelBuilder.ApplyConfiguration(new DesignerReviewConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyFeatureConfiguration());
            modelBuilder.ApplyConfiguration(new ReportOrderConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyPriceHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new BookingPaymentConfiguration());
            modelBuilder.ApplyConfiguration(new RenovationSimulationConfiguration());
            modelBuilder.ApplyConfiguration(new SimulationMediaConfiguration());
            modelBuilder.ApplyConfiguration(new SimulationRecommendationConfiguration());
            modelBuilder.ApplyConfiguration(new UserChatQuotaConfiguration());




            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
        .Property(u => u.PasswordHash)
        .IsRequired(false);

            modelBuilder.Entity<User>().ToTable("Users");
        }

    }
}