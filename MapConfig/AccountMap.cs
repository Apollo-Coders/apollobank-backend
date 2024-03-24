using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ApolloBank.Models;

namespace ApolloBank.MapConfig
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.
                HasKey(a => a.Id);
            builder
                .HasOne(a => a.CreditCards)
                .WithOne(c => c.Account)
                .HasForeignKey<CreditCards>(a => a.AccountId);
            builder
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId);
            builder
                .HasOne(a => a.User)
                .WithOne(u => u.Account)
                .HasForeignKey<User>(u => u.AccountId);
        }
    }
}
