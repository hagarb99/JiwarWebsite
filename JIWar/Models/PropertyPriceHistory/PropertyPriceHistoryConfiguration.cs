using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiwar.Models
{
    public class PropertyPriceHistoryConfiguration : IEntityTypeConfiguration<PropertyPriceHistory>
    {
        public void Configure(EntityTypeBuilder<PropertyPriceHistory> builder)
        {
            builder.HasKey(p => p.PropertyPriceHistoryId);

            builder.Property(p => p.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.DateRecorded)
                   .IsRequired();

            builder.HasOne(p => p.Property)
                   .WithMany(prop => prop.PriceHistory)
                   .HasForeignKey(p => p.PropertyID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("PropertyPriceHistories");

        }
    }
}
