using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GEWAR.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("Subscriptions");

            // 🔑 Primary Key
            builder.HasKey(s => s.Id);

            // 🧱 Properties
            builder.Property(s => s.PlanType)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(s => s.StartDate)
                   .IsRequired()
                   .HasColumnType("datetime");

            builder.Property(s => s.EndDate)
                   .IsRequired()
                   .HasColumnType("datetime");

            builder.Property(s => s.Status)
                   .IsRequired()
                   .HasMaxLength(30);

            // 👤 Relationship with User
            builder.HasOne<User>()
                   .WithMany() // or .WithMany(u => u.Subscriptions) if you add that navigation
                   .HasForeignKey(s => s.UserID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
