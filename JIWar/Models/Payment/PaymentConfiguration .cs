using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiwar.Models
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment");

            builder.HasKey(p => p.PaymentID);

            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.PaymentDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.paymentMethod)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Status)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
