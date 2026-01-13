using JIWAR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEWAR.Models;


namespace Jiwar.Models
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            // Table name and schema 
            builder.ToTable("Booking", "Transactions");

            // Primary key
            builder.HasKey(b => b.Id);

            // Columns
            builder.Property(b => b.PropertyID)
                   .IsRequired();

            builder.Property(b => b.CustomerID)
                   .IsRequired();

            builder.Property(b => b.OfferID)
                   .IsRequired(false);

            builder.Property(b => b.StartDate)
                   .HasColumnType("datetime")
                   .IsRequired(false);

            builder.Property(b => b.EndDate)
                   .HasColumnType("datetime")
                   .IsRequired(false);

            builder.Property(b => b.PaymentStatus)
        .HasConversion<string>()
        .HasMaxLength(50)
        .IsRequired(false);


            builder.HasOne(b => b.BookingRating)
       .WithOne(br => br.Booking)
       .HasForeignKey<BookingRating>(br => br.BookingID)
       .OnDelete(DeleteBehavior.Cascade);

            builder.Property(b => b.status)
       .HasConversion<string>();

            builder.Property(b => b.Cost)
       .HasColumnType("decimal(18,2)")
       .IsRequired();
           

            builder.HasOne(b => b.Customer)
                   .WithMany()
                   .HasForeignKey(b => b.CustomerID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Offer)
                   .WithMany()
                   .HasForeignKey(b => b.OfferID)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.Property(b => b.PaymentMethod)
       .HasConversion<string>()
       .HasMaxLength(30)
       .IsRequired();


        }


    }
}
