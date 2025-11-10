using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GEWAR.Models
{
    public class PropertyMediaConfiguration : IEntityTypeConfiguration<PropertyMedia>
    {
        public void Configure(EntityTypeBuilder<PropertyMedia> builder)
        {
            //  Table name and schema
            builder.ToTable("PropertyMedia", "Property");

            //  Primary key
            builder.HasKey(pm => pm.Id);

            //  PropertyID (foreign key)
            builder.Property(pm => pm.PropertyID)
                   .IsRequired();

            //  MediaURL
            builder.Property(pm => pm.MediaURL)
                   .HasMaxLength(500)
                   .IsRequired();

            //  MediaType
            builder.Property(pm => pm.MediaType)
                   .HasMaxLength(50)
                   .IsRequired();

            //  UploadedDate
            builder.Property(pm => pm.UploadedDate)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            //  Relationship (Property 1 → * PropertyMedia)
            builder.HasOne<Property>()
                   .WithMany(p => p.PropertyMedia) // تأكدي إنك ضفتي ICollection<PropertyMedia> في كلاس Property
                   .HasForeignKey(pm => pm.PropertyID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

