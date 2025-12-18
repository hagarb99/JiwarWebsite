using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GEWAR.Models.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            // Table name & schema
            builder.ToTable("Properties", "RealEstate");

            // Primary Key
            builder.HasKey(p => p.PropertyID);

            // Columns
            builder.Property(p => p.OwnerID)
                   .IsRequired();

            builder.Property(p => p.Address)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(p => p.City)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Area_sqm)
                   .HasColumnType("decimal(10,2)");

            builder.Property(p => p.NumBedrooms)
                   .IsRequired(false);

            builder.Property(p => p.NumBathrooms)
                   .IsRequired(false);



            builder.Property(p => p.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");



            //  Relationships

            // 1️ Property ↔ Offers (One-to-Many)
            builder.HasMany(p => p.Offers)
                   .WithOne(o => o.Property)
                   .HasForeignKey(o => o.PropertyID)
                   .OnDelete(DeleteBehavior.Cascade);

            // 2️ Property ↔ PortfolioProperty (Many-to-Many via join table)
            builder.HasMany(p => p.PortfolioProperties)
                   .WithOne(pp => pp.Property)
                   .HasForeignKey(pp => pp.PropertyID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.PropertyOwner)
        .WithMany(po => po.Properties)
        .HasForeignKey(p => p.OwnerID)
        .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.statusEnum)
       .HasConversion<string>()
       .HasMaxLength(50)
       .IsRequired();

          

            builder.Property(p => p.LocationLat)
                   .HasColumnType("decimal(10,6)");

            builder.Property(p => p.LocationLang)
                   .HasColumnType("decimal(10,6)");

           
           builder.HasMany(p => p.RenovationProjects)
             .WithOne()
             .HasForeignKey(rp => rp.PropertyID)
             .OnDelete(DeleteBehavior.Restrict);





        }
    }
}

