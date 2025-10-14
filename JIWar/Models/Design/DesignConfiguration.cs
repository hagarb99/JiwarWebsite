using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiwar.Models
{
    public class DesignConfiguration : IEntityTypeConfiguration<Design>
    {
        public void Configure(EntityTypeBuilder<Design> builder)
        {
            //  Table name
            builder.ToTable("Design");

            //  Primary Key
            builder.HasKey(d => d.Id);

            //  Properties
            builder.Property(d => d.DesignURL)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(d => d.SelectedStyle)
                   .HasMaxLength(100);

            builder.Property(d => d.AI_Generated)
                   .HasDefaultValue(false);

            builder.Property(d => d.CreationDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            //  Relationships

            // Each Design belongs to one Request
            builder.HasOne(d => d.Request)
                   .WithMany()
                   .HasForeignKey(d => d.RequestID)
                   .OnDelete(DeleteBehavior.Restrict);

            // Each Design belongs to one Interior Designer
            builder.HasOne(d => d.InteriorDesigner)
                   .WithMany()
                   .HasForeignKey(d => d.DesignerID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
