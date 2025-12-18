using Jiwar.Models.Valuation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GEWAR.Models.Configurations
{
    public class ValuationHistoryConfiguration : IEntityTypeConfiguration<ValuationHistory>
    {
        public void Configure(EntityTypeBuilder<ValuationHistory> builder)
    {
        builder.ToTable("ValuationHistory", "Analytics");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.MostLikelyPrice)
               .HasColumnType("decimal(18,2)");

        builder.Property(v => v.MinPrice)
               .HasColumnType("decimal(18,2)");

        builder.Property(v => v.MaxPrice)
               .HasColumnType("decimal(18,2)");

        builder.Property(v => v.ConfidenceScore)
               .HasColumnType("decimal(5,2)");

        builder.Property(v => v.FactorBreakdownJson)
               .HasColumnType("nvarchar(max)");

        builder.HasOne(v => v.User)
               .WithMany()
               .HasForeignKey(v => v.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
    }
    

}