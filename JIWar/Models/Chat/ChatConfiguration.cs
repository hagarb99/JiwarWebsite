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
            //  Table name and schema
            builder.ToTable("Chat", "Messaging");

            //  Primary Key
            builder.HasKey(c => c.Id);

            //  Properties
            builder.Property(c => c.MessageText)
                   .IsRequired()
                   .HasColumnType("text");

            builder.Property(c => c.MessageType)
                   .IsRequired()
                   .HasConversion<string>()  
                   .HasMaxLength(20);

            builder.Property(c => c.SentDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            //  Relationships

            // Each Chat has one Sender (User)
            builder.HasOne(c => c.Sender)
                   .WithMany()
                   .HasForeignKey(c => c.SenderID)
                   .OnDelete(DeleteBehavior.Restrict);

            // Each Chat has one Receiver (User)
            builder.HasOne(c => c.Receiver)
                   .WithMany()
                   .HasForeignKey(c => c.ReceiverID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
