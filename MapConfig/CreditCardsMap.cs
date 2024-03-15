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
            
        }
    }
}
