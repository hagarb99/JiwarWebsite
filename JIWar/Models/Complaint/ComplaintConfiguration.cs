using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
    public class ComplaintConfiguration : IEntityTypeConfiguration<Complaint>
    {
        public void Configure(EntityTypeBuilder<Complaint> builder)
        {
            // 🔹 Table name
            builder.ToTable("Complaint");

            // 🔹 Primary Key
            builder.HasKey(c => c.Id);

            // 🔹 Properties configuration
            builder.Property(c => c.ComplaintText)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(c => c.Status)
       .HasConversion<string>()
       .HasMaxLength(50)
       .IsRequired();


            builder.Property(c => c.CreatedDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            //  Relationships

            // Each Complaint belongs to one User (who submitted it)
            builder.HasOne(c => c.User)
                   .WithMany()
                   .HasForeignKey(c => c.UserID)
                   .OnDelete(DeleteBehavior.Restrict);

            // Each Complaint may be related to one Designer (optional)
            builder.HasOne(c => c.Designer)
                   .WithMany()
                   .HasForeignKey(c => c.DesignerID)
                   .OnDelete(DeleteBehavior.Restrict);

            // Each Complaint may be related to one Booking (optional)
            builder.HasOne(c => c.Booking)
                   .WithMany()
                   .HasForeignKey(c => c.BookingID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
