using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ApolloBank.Models;

namespace ApolloBank.MapConfig
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Amount).IsRequired().HasPrecision(10, 2);
            builder.Property(u => u.Description).HasMaxLength(100).HasColumnType("varchar(100)");
            builder.Property(u => u.TransactionType).IsRequired();
            builder.Property(u => u.Direction).HasMaxLength(1).HasColumnType("char(1)");
        }
    }
}
