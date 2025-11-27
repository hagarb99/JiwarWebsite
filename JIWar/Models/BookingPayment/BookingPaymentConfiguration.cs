using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiwar.Models
{
    public class BookingPaymentConfiguration : IEntityTypeConfiguration<BookingPayment>
    {
        public void Configure(EntityTypeBuilder<BookingPayment> builder)
        {
            builder.ToTable("BookingPayments");

            builder.HasKey(bp => bp.Id);

            builder.Property(bp => bp.Amount)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.Property(bp => bp.PaymentMethod)
                   .HasConversion<string>() 
                   .IsRequired();

            builder.Property(bp => bp.PaymentStatus)
                   .HasConversion<string>() 
                   .IsRequired();

            builder.Property(bp => bp.PaymentReference)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(bp => bp.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(bp => bp.User)
                   .WithMany()
                   .HasForeignKey(bp => bp.UserID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bp => bp.Booking)
                   .WithMany()
                   .HasForeignKey(bp => bp.BookingID)
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
