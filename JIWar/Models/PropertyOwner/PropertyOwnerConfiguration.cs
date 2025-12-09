using GEWAR.Models;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PropertyOwnerConfiguration : IEntityTypeConfiguration<PropertyOwner>
{
    public void Configure(EntityTypeBuilder<PropertyOwner> builder)
    {
        builder.ToTable("PropertyOwners");

        // 🔑 Primary Key
        builder.HasKey(po => po.UserID);
        builder.Property(po => po.UserID)
               .IsRequired(); 

        // 🔗 علاقة مع User (One-to-One)
        builder.HasOne(po => po.Owneruser)
               .WithOne(u => u.propertyOwner)
               .HasForeignKey<PropertyOwner>(po => po.UserID)
               .OnDelete(DeleteBehavior.Restrict);

        // 🔗 علاقة مع Property (One-to-Many)
        builder.HasMany(po => po.Properties)
               .WithOne(p => p.PropertyOwner)
               .HasForeignKey(p => p.OwnerID)
               .OnDelete(DeleteBehavior.Restrict);

        // 🎯 ENUMS كـ string
        builder.Property(po => po.planTypeEnum)
               .HasConversion<string>()
               .HasMaxLength(50);

        builder.Property(po => po.statusEnum2)
               .HasConversion<string>()
               .HasMaxLength(50);
    }
}
