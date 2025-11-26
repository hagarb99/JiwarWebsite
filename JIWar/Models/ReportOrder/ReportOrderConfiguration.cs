using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiwar.Models
{
    public class ReportOrderConfiguration : IEntityTypeConfiguration<ReportOrder>
    {
        public void Configure(EntityTypeBuilder<ReportOrder> builder)
        {
            builder.ToTable("ReportOrders");

            builder.HasKey(ro => ro.Id);

            builder.Property(ro => ro.Amount)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.Property(ro => ro.PaymentMethod)
                   .HasConversion<string>() 
                   .IsRequired();

            builder.Property(ro => ro.PaymentStatus)
                   .HasConversion<string>() 
                   .IsRequired();

            builder.Property(ro => ro.PaymentReference)
                   .HasMaxLength(100);

            builder.Property(ro => ro.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(ro => ro.User)
                   .WithMany()
                   .HasForeignKey(ro => ro.UserID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ro => ro.Report)
                   .WithMany()
                   .HasForeignKey(ro => ro.ReportID)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
