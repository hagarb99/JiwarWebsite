using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GEWAR.Configurations
{
    public class RequestRatingConfiguration : IEntityTypeConfiguration<RequestRating>
    {
        public void Configure(EntityTypeBuilder<RequestRating> builder)
        {
            // Table name and schema
            builder.ToTable("RequestRatings", "Ratings");

            // Primary Key
            builder.HasKey(rr => rr.Id);

            // Properties
            builder.Property(rr => rr.Rating)
                   .IsRequired(); // يجب أن يكون بين 1 - 5 (يمكن إضافة Constraint لاحقاً)

            builder.Property(rr => rr.Comment)
                   .HasMaxLength(500)
                   .IsRequired(false);

            builder.Property(rr => rr.CreatedDate)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            // ✅ Relationships

            builder.HasOne(rr => rr.Request)
                   .WithMany()
                   .HasForeignKey(rr => rr.RequestID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rr => rr.User)
                   .WithMany()
                   .HasForeignKey(rr => rr.UserID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rr => rr.InteriorDesigner)
                   .WithMany()
                   .HasForeignKey(rr => rr.DesignerID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
