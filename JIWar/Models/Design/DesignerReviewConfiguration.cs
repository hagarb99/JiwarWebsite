using Jiwar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GEWAR.Models.Configurations
{
    public class DesignerReviewConfiguration : IEntityTypeConfiguration<DesignerReview>
    {
        public void Configure(EntityTypeBuilder<DesignerReview> builder)
        {
            builder.ToTable("DesignerReviews");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.DesignerID).IsRequired();
            builder.Property(r => r.PropertyOwnerID).IsRequired();
            builder.Property(r => r.ProposalID).IsRequired();
            builder.Property(r => r.DesignRequestID).IsRequired();
            builder.Property(r => r.Rating).IsRequired();
            builder.Property(r => r.Comment).HasMaxLength(1000);
            builder.Property(r => r.CreatedAt).IsRequired();

            builder.HasOne(r => r.Designer)
                .WithMany()
                .HasForeignKey(r => r.DesignerID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.PropertyOwner)
                .WithMany()
                .HasForeignKey(r => r.PropertyOwnerID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Proposal)
                .WithMany()
                .HasForeignKey(r => r.ProposalID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.DesignRequest)
                .WithMany()
                .HasForeignKey(r => r.DesignRequestID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
