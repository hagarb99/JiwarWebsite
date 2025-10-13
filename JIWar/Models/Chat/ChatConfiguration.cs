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
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("Chat", "Messaging");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.MessageText)
                   .IsRequired()
                   .HasColumnType("text"); // محتوى الرسالة

            builder.Property(c => c.MessageType)
                   .IsRequired()
                   .HasConversion<string>()   
                   .HasMaxLength(20);

            builder.Property(c => c.SentDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

        }
    }

}
