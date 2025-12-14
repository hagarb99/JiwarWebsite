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
    public class InteriorDesignerConfiguration : IEntityTypeConfiguration<InteriorDesigner>
    {
        public void Configure(EntityTypeBuilder<InteriorDesigner> builder)
        {
            builder.ToTable("InteriorDesigners");

            // 🔑 Primary Key
            builder.HasKey(id => id.InteriorDesignerID);

            builder.Property(id => id.InteriorDesignerID)
                   .IsRequired();

            // 🔗 One-to-One مع User
            builder.HasOne(id => id.User)
                   .WithOne(u => u.InteriorDesigner)
                   .HasForeignKey<InteriorDesigner>(id => id.InteriorDesignerID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(id => id.Specialization)
                   .HasMaxLength(200);

            builder.Property(id => id.PortfolioURL)
                   .HasMaxLength(500);
        }
    }
}
