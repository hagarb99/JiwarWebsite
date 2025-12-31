using Jiwar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiwar.Models
{
    public class DesignRequestConfiguration : IEntityTypeConfiguration<DesignRequest>
    {
        public void Configure(EntityTypeBuilder<DesignRequest> builder)
        {
            builder.ToTable("DesignRequests", "Design");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.UserID)
                   .IsRequired();

            builder.Property(d => d.PropertyID)
                   .IsRequired();

            builder.Property(d => d.PreferredStyle)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(d => d.Budget)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired(false);

            builder.Property(d => d.Notes)
                   .HasMaxLength(2000)
                   .IsRequired(false);

            builder.Property(d => d.Status)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(d => d.CreatedAt)
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.HasOne(d => d.User).WithMany(u => u.DesignRequests)
                   .HasForeignKey(d => d.UserID)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Property).WithMany(u => u.DesignRequests)
                 .HasForeignKey(d => d.PropertyID)
                 .OnDelete(DeleteBehavior.NoAction);

            

        }
    }
}

