using Jiwar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiwar.Models
{
    public class DesignerProposalConfiguration : IEntityTypeConfiguration<DesignerProposal>
    {
        public void Configure(EntityTypeBuilder<DesignerProposal> builder)
        {
            builder.ToTable("DesignerProposals", "dbo");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.DesignRequestID)
                   .IsRequired();

            builder.Property(p => p.DesignerID)
                   .IsRequired();

            builder.Property(p => p.EstimatedCost)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(p => p.EstimatedDays)
                   .IsRequired();

            builder.Property(p => p.ProposalDescription)
                   .HasMaxLength(2000)
                   .IsRequired(false);

            builder.Property(p => p.SampleDesignURL)
                   .HasMaxLength(500)
                   .IsRequired(false);

            builder.Property(p => p.Status)
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
