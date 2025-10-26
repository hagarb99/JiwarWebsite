using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GEWAR.Configurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            // Table Name + Schema
            builder.ToTable("Requests", "Transactions");

            // Primary Key
            builder.HasKey(r => r.Id);

            // Properties
            builder.Property(r => r.CustomerID)
                   .IsRequired();

            builder.Property(r => r.PropertyID)
                   .IsRequired();

            builder.Property(r => r.Description)
                   .HasMaxLength(500)
                   .IsRequired(false);  // Optional description

            builder.Property(r => r.Status)
                   .HasMaxLength(50)
                   .IsRequired(); // Must be provided

            builder.Property(r => r.CreatedDate)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETUTCDATE()") // Auto insert date
                   .IsRequired();

            // ✅ Future navigation properties (if added later)
            builder.HasOne(r => r.user)
                   .WithMany()
                   .HasForeignKey(r => r.CustomerID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.propertie)
                   .WithMany()
                   .HasForeignKey(r => r.PropertyID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
