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

            builder.Property(p => p.FinishingStatus)
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.Property(p => p.PropertyType)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(p => p.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.Status)
                   .HasMaxLength(50)
                   .IsRequired();

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
        }
    }
}

