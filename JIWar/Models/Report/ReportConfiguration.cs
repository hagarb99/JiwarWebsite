using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GEWAR.Configurations
    {
        public class ReportConfiguration : IEntityTypeConfiguration<Report>
        {
            public void Configure(EntityTypeBuilder<Report> builder)
            {
                builder.ToTable("Reports", "Complaints");

                builder.HasKey(r => r.Id);

                builder.Property(r => r.ReportType)
                       .IsRequired()
                       .HasMaxLength(100);

                builder.Property(r => r.Description)
                       .IsRequired()
                       .HasColumnType("text");

                builder.Property(r => r.Status)
                       .IsRequired()
                       .HasMaxLength(50)
                       .HasDefaultValue("Pending");

                builder.Property(r => r.CreatedDate)
                       .IsRequired()
                       .HasDefaultValueSql("GETUTCDATE()");

                builder.HasOne(r => r.User)
                       .WithMany()
                       .HasForeignKey(r => r.UserID)
                       .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(r => r.Booking)
                       .WithMany()
                       .HasForeignKey(r => r.BookingID)
                       .OnDelete(DeleteBehavior.SetNull);
            }
        }
    }
