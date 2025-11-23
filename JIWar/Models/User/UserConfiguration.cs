using GEWAR.Models;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace GEWAR.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id); // Assuming BaseModel has ID as primary key

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(100);


            builder.Property(u => u.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(u => u.ProfilePicURL)
                   .HasMaxLength(250).IsRequired(false);


            builder.Property(u => u.Role)
                 .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Role)
                .HasColumnType("nvarchar(50)")
                   .IsRequired();



            builder.Property(u => u.RegistrationDate)
                   .IsRequired();

            // ✅ Relationships
            builder.HasMany(u => u.InvestmentPortfolios)
                   .WithOne(ip => ip.User)
                   .HasForeignKey(ip => ip.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(u => u.Offers)
            //       .WithOne(o => o.User)
            //       .HasForeignKey(o => o.UserID)
            //       .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Notifications)
                   .WithOne(n => n.User)
                   .HasForeignKey(n => n.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Payments)
                   .WithOne(p => p.User)
                   .HasForeignKey(p => p.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.InteriorDesigner)
         .WithOne(d => d.User)   // يشير للـ navigation property داخل InteriorDesigner
         .HasForeignKey<InteriorDesigner>(d => d.UserID) // يشير للـ FK داخل InteriorDesigner
         .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
