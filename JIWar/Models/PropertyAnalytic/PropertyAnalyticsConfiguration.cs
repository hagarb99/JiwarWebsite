using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Jiwar.Models
{
    public class PropertyAnalyticsConfiguration : IEntityTypeConfiguration<PropertyAnalytics>
    {
        public void Configure(EntityTypeBuilder<PropertyAnalytics> builder)
        {
            // Table name and schema
            builder.ToTable("PropertyAnalytics", "Analytics");

            // Primary key
            builder.HasKey(p => p.Id);

            // Columns
            builder.Propertie(p => p.FairValue_Estimate)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired(false);

            builder.Propertie(p => p.Price_Influence_Factors)
                   .HasColumnType("nvarchar(max)")
                   .IsRequired(false);

            builder.Propertie(p => p.AnalysisDate)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            builder.HasOne(p => p.Propertie)
                   .WithMany(prop => prop.PropertieAnalytics) // ⬅ لازم تضيفي الـ ICollection في كلاس Propertie
                   .HasForeignKey(p => p.PropertieID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
