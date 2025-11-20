using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GEWAR.Models
{
    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            // اسم الجدول
            builder.ToTable("Features");

            // Primary Key
            builder.HasKey(f => f.Id);

            // Name Column
            builder.Property(f => f.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // العلاقة مع PropertyFeature
            builder.HasMany(f => f.PropertyFeatures)
                   .WithOne(pf => pf.Feature)
                   .HasForeignKey(pf => pf.FeatureId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}