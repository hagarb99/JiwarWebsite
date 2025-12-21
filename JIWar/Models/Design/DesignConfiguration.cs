using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Jiwar.DTOs.DesignDto;


namespace Jiwar.Models
{
    public class DesignConfiguration : IEntityTypeConfiguration<Design>
    {
        public void Configure(EntityTypeBuilder<Design> builder)
        {
            builder.ToTable("Designs", "Design");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.DesignerID)
                   .IsRequired();

            builder.Property(d => d.PropertyID)
                   .IsRequired();

            builder.Property(d => d.AI_Generated)
                   .IsRequired();

            builder.Property(d => d.SelectedStyle)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(d => d.CreationDate)
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(d => d.ProposalID)
                   .IsRequired(false);
        }
    }
}

