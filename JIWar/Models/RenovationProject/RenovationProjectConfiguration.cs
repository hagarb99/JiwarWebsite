using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GEWAR.Models.Configurations
{
    public class RenovationProjectConfiguration : IEntityTypeConfiguration<RenovationProject>
    {
        public void Configure(EntityTypeBuilder<RenovationProject> builder)
        {
            builder.ToTable("RenovationProjects");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EstimatedCost)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.EstimatedProfit)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.RenovationCosts)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.ProjectStatus)
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.Property(x => x.StartDate)
                   .IsRequired(false);

            builder.Property(x => x.EndDate)
                   .IsRequired(false);
        }
    }
}
