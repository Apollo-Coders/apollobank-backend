using ApolloBank.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApolloBank.MapConfig
{
    public class CreditCardMap : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.ToTable("CreditCard");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsBlocked);

            builder.Property(x => x.Number);

            builder.Property(x => x.Cvc);

            builder.Property(x => x.ExpirationTime);

            builder.Property(x => x.CreditUsed);

            builder.Property(x => x.CreditLimit);

            builder.Property(x => x.AccountId).HasColumnName("AccountId");

            /*builder.HasOne(u => u.Account)
            .WithOne()
            .HasForeignKey<CreditCard>(a => a.Account_Id)
            .OnDelete(DeleteBehavior.Cascade);*/
        }
    }
}
