using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiwar.Models.Offers
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.ToTable("Offers");

            builder.HasKey(o => o.OfferID);

            builder.Property(o => o.OfferAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(o => o.OfferStatus)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(o => o.OfferDate)
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(o => o.Buyer)
                .WithMany(u => u.Offers)
                .HasForeignKey(o => o.BuyerID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Propertie)
                .WithMany(p => p.Offers)
                .HasForeignKey(o => o.PropertyID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
