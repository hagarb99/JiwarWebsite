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
            builder.Property(s => s.planTypeEnum)
       .HasConversion<string>()
       .HasMaxLength(50)
       .IsRequired();

            builder.Property(s => s.StartDate)
                   .IsRequired()
                   .HasColumnType("datetime");

            builder.Property(s => s.EndDate)
                   .IsRequired()
                   .HasColumnType("datetime");

            builder.Property(s => s.statusEnum2)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .IsRequired();

            // 👤 Relationship with User
            builder.HasOne(s => s.User)
        .WithMany(u => u.Subscriptions)
        .HasForeignKey(s => s.UserID)
        .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
