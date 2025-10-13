using GEWAR.Models.BookingRating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiwar.Models
{
    public class BookingRatingConfiguration : IEntityTypeConfiguration<BookingRating>
    {
        public void Configure(EntityTypeBuilder<BookingRating> builder)
        {
           
            builder.ToTable("BookingRatings", "Ratings");

           
            builder.HasKey(br => br.Id);

         
            builder.Property(br => br.Rating)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(br => br.Comment)
                   .HasMaxLength(500)
                   .IsRequired(false);

            builder.Property(br => br.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");


            builder.HasOne(br => br.Booking)
                   .WithOne()  
                   .HasForeignKey<BookingRating>(br => br.BookingID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(br => br.User)
                   .WithMany() 
                   .HasForeignKey(br => br.UserID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
