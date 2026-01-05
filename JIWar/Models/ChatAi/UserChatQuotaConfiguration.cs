using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiwar.Models.ChatAi
{
    public class UserChatQuotaConfiguration : IEntityTypeConfiguration<UserChatQuota>
    {
        public void Configure(EntityTypeBuilder<UserChatQuota> builder)
        {
            // 📌 Table
            builder.ToTable("UserChatQuotas", "AI");

            // 🔑 Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            // 👤 UserId
            builder.Property(x => x.UserId)
                   .IsRequired()
                   .HasMaxLength(450); // مناسب لـ Identity UserId

            // 🧮 Remaining Messages
            builder.Property(x => x.RemainingMessages)
                   .IsRequired();

            // ⏰ Last Reset
            builder.Property(x => x.LastReset)
                   .IsRequired();

            // 🗑 Soft Delete
            builder.Property(x => x.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            // 🔒 One quota per user (important)
            builder.HasIndex(x => x.UserId)
                   .IsUnique();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
