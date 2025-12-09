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
            builder.ToTable("InteriorDesigner");

            builder.HasKey(d => d.UserID);

            builder.Property(d => d.Specialization)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(d => d.PortfolioURL)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasOne(d => d.User)
                .WithOne()
                .HasForeignKey<InteriorDesigner>(d => d.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
