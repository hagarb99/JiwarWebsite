using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GEWAR.Data.Configurations
{
    public class RenovationSimulationConfiguration : IEntityTypeConfiguration<RenovationSimulation>
    {
        public void Configure(EntityTypeBuilder<RenovationSimulation> builder)
        {
            builder.ToTable("RenovationSimulations");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.BudgetMin)
                   .HasColumnType("decimal(18,2)");

            builder.Property(r => r.BudgetMax)
                   .HasColumnType("decimal(18,2)");

            builder.Property(r => r.RenovationGoalsJson)
                   .HasColumnType("nvarchar(max)");

            builder.Property(r => r.Status)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(r => r.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            builder.HasOne(r => r.Property)
                   .WithMany(p => p.RenovationSimulations)
                   .HasForeignKey(r => r.PropertyID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Recommendations)
                   .WithOne(rec => rec.RenovationSimulation)
                   .HasForeignKey(rec => rec.RenovationSimulationID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
