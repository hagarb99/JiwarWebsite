using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiwar.Models
{
    public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
    {
        public void Configure(EntityTypeBuilder<Proposal> builder)
        {
            // Table name & schema
            builder.ToTable("Proposals", "Requests");

            // Primary key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.OfferDetails)
                   .HasMaxLength(1000)
                   .IsRequired(false);

            builder.Property(p => p.PriceEstimate)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired(false);

            builder.Property(p => p.Status)
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.Property(p => p.CreatedDate)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.StatusEnumReq)
                   .HasConversion<string>()
                   .HasMaxLength(50)
                   .IsRequired();

            // Relationships
            builder.HasOne<Request>() // العلاقة مع الـ Request
                   .WithMany()
                   .HasForeignKey(p => p.RequestID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<InteriorDesigner>() // العلاقة مع الـ Designer
                   .WithMany()
                   .HasForeignKey(p => p.DesignerID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
