using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiwar.Models
{
    public class RenovationCostConfiguration : IEntityTypeConfiguration<RenovationCost>
    {
        public void Configure(EntityTypeBuilder<RenovationCost> builder)
        {
            builder.ToTable("RenovationCost", "Management");

            builder.HasKey(rc => rc.Id);

            builder.Property(rc => rc.CostType)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(rc => rc.EstimatedValue)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired(false);

            builder.HasIndex(rc=>rc.RenovationProjectID);      

            // Relationship with RenovationProject
            builder.HasOne(rc => rc.RenovationProject)
        .WithMany(rp => rp.RenovationCosts) // ← اربطي بـ collection property
        .HasForeignKey(rc => rc.RenovationProjectID)
        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
