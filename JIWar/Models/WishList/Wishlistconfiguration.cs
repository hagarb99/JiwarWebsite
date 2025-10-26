using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GEWAR.Models;

namespace Jiwar.Models
{
    public class WishListConfiguration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            // Table & Schema
            builder.ToTable("WishList", "UserData");

            // Primary Key
            builder.HasKey(w => w.Id);

            // Columns
            builder.Property(w => w.AddedDate)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            builder.Property(w => w.Notes)
                   .HasMaxLength(500)
                   .IsRequired(false);

            // Relationships
            builder.HasOne(w => w.User)
                   .WithMany() // or .WithMany(u => u.WishLists) if added in User
                   .HasForeignKey(w => w.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(w => w.Property)
                   .WithMany() // or .WithMany(p => p.WishLists)
                   .HasForeignKey(w => w.PropertyID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
