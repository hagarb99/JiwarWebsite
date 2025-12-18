using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GEWAR.Data.Configurations
{
    public class SimulationMediaConfiguration : IEntityTypeConfiguration<SimulationMedia>
    {
        public void Configure(EntityTypeBuilder<SimulationMedia> builder)
        {
            builder.ToTable("SimulationMedias");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.FileUrl)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(m => m.MediaType)
                   .HasConversion<string>()
                   .IsRequired();

            builder.HasOne(m => m.RenovationSimulation)
                   .WithMany(r => r.Medias) // لو في علاقة مباشرة، ممكن نعمل ICollection<SimulationMedia> في RenovationSimulation
                   .HasForeignKey(m => m.RenovationSimulationID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
