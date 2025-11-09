using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GEWAR.Models.Configurations
{
    public class PortfolioPropertyConfiguration : IEntityTypeConfiguration<PortfolioProperty>
    {
        public void Configure(EntityTypeBuilder<PortfolioProperty> builder)
        {
            // Table name
            builder.ToTable("PortfolioProperties");

            // Primary Key
            builder.HasKey(p => p.PortfolioPropertyID);

            // Relationships
            builder.HasOne(p => p.Portfolio)
                   .WithMany(port => port.PortfolioProperties)
                   .HasForeignKey(p => p.PortfolioID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Propertie)
                   .WithMany(prop => prop.PortfolioProperties)
                   .HasForeignKey(p => p.PropertyID)
                   .OnDelete(DeleteBehavior.Restrict);

            // Property configurations
            builder.Property(p => p.OriginalPurchasePrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(p => p.CalculatedROI)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();
        }
    }
}

