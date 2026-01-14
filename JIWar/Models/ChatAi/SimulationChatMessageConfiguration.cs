using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiwar.Models.ChatAi
{
    public class SimulationChatMessageConfiguration : IEntityTypeConfiguration<SimulationChatMessage>
    {
        public void Configure(EntityTypeBuilder<SimulationChatMessage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.HasOne(x => x.RenovationSimulation)
                   .WithMany(x=>x.SimulationChatMessages)
                   .HasForeignKey(x => x.RenovationSimulationID)
                   .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(x => x.User)
                   .WithMany(x => x.SimulationChatMessages)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
