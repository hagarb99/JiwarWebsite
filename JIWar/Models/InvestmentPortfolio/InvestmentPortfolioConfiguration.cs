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
    public class InvestmentPortfolioConfiguration : IEntityTypeConfiguration<InvestmentPortfolio>
    {
        public void Configure(EntityTypeBuilder<InvestmentPortfolio> builder)
        {
            builder.ToTable("InvestmentPortfolio");

            builder.HasKey(p => p.PortfolioID);

            builder.Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.CreationDate)
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(p => p.User)
                .WithMany(u => u.InvestmentPortfolios)
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
