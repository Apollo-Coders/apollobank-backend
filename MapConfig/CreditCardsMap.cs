using ApolloBank.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ApolloBank.MapConfig
{
    public class CreditCardsMap : IEntityTypeConfiguration<CreditCards>
    {
        public void Configure(EntityTypeBuilder<CreditCards> builder)
        {
            builder.ToTable("CreditCards");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TotalCreditLimit);

            builder.Property(x => x.TotalCreditUsed);

            builder.Property(x => x.TotalAlocatedCredit);
        }
    }
}
