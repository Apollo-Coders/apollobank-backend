using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ApolloBank.Models;

namespace ApolloBank.MapConfig
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {

            builder
                .HasOne(a => a.CreditCards)
                .WithOne(c => c.Account)
                .HasForeignKey<CreditCards>(a => a.Account_Id);
            builder
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.Account_Id);
            builder
                .HasOne(a => a.User)
                .WithOne(u => u.Account)
                .HasForeignKey<User>(u => u.AccountId);
        }
    }
}
