using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GEWAR.Models
{
    public class PropertyFeatureConfiguration : IEntityTypeConfiguration<PropertyFeature>
    {
        public void Configure(EntityTypeBuilder<PropertyFeature> builder)
        {
            // اسم الجدول
            builder.ToTable("PropertyFeature");

            // Primary Key مركّب
            builder.HasKey(pf => new { pf.PropertyId, pf.FeatureId });

            // علاقة Property 1 - M PropertyFeature
            builder.HasOne(pf => pf.Property)
                .WithMany(p => p.PropertyFeatures)
                .HasForeignKey(pf => pf.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            // علاقة Feature 1 - M PropertyFeature
            builder.HasOne(pf => pf.Feature)
                .WithMany(f => f.PropertyFeatures)
                .HasForeignKey(pf => pf.FeatureId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }



}