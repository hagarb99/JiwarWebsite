using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GEWAR.Data.Configurations
{
    public class SimulationRecommendationConfiguration : IEntityTypeConfiguration<SimulationRecommendation>
    {
        public void Configure(EntityTypeBuilder<SimulationRecommendation> builder)
        {
            builder.ToTable("SimulationRecommendations");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(r => r.Description)
                   .HasColumnType("nvarchar(max)");

            builder.Property(r => r.Category)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(r => r.Severity)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(r => r.IsAIGenerated)
                   .HasDefaultValue(false);

            builder.HasOne(r => r.RenovationSimulation)
                   .WithMany(sim => sim.Recommendations)
                   .HasForeignKey(r => r.RenovationSimulationID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
