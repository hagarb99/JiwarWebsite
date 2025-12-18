using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GEWAR.Models.Configurations
{
    public class RenovationProjectConfiguration : IEntityTypeConfiguration<RenovationProject>
    {
        public void Configure(EntityTypeBuilder<RenovationProject> builder)
        {
            builder.ToTable("RenovationProjects" , "Management");

            builder.HasKey(x => x.Id);

            builder.Property(x=>x.UserID)
            .IsRequired();

            builder.Property(x=>x.PropertyID)
            .IsRequired();

            builder.Property(x => x.EstimatedCost)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.EstimatedProfit)
                   .HasColumnType("decimal(18,2)");


            //Relationships
            builder.HasMany(rp => rp.RenovationCosts)
       .WithOne(rc => rc.RenovationProject)
       .HasForeignKey(rc => rc.RenovationProjectID)
       .OnDelete(DeleteBehavior.Cascade);


            builder.Property(rp => rp.ProjectStatusEnum)
       .HasConversion<string>()
       .HasMaxLength(50)
       .IsRequired(false);

                   

            builder.Property(x => x.StartDate)
                   .IsRequired(false);

            builder.Property(x => x.EndDate)
                   .IsRequired(false);
        }
    }
}
