using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GEWAR.Models;

namespace Jiwar.Models
{
    public class VirtualTourConfiguration : IEntityTypeConfiguration<VirtualTour>
    {
        public void Configure(EntityTypeBuilder<VirtualTour> builder)
        {
            // Table & Schema
            builder.ToTable("VirtualTour", "Media");

            // Primary Key
            builder.HasKey(v => v.Id);

            // Columns
            builder.Property(v => v.TourURL)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(v => v.TourTitle)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(v => v.Description)
                   .HasMaxLength(1000);

            builder.Property(v => v.CreatedDate)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            // Relationships
            builder.HasOne(v => v.Property)
                   .WithMany() // if Property has many tours
                   .HasForeignKey(v => v.PropertyID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.User)
                   .WithMany()
                   .HasForeignKey(v => v.UserID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
