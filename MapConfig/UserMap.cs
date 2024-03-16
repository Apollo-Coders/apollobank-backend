using ApolloBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; // Import the namespace that contains the User class

namespace ApolloBank.MapConfig
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FullName).IsRequired().HasMaxLength(100);

            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.Password).IsRequired().HasMaxLength(255);

            builder.Property(u => u.DDD).IsRequired();

            builder.Property(u => u.PhoneNumber).IsRequired();

            builder.Property(u => u.BirthDay);

            builder.Property(u => u.CPF).IsRequired().HasMaxLength(11);
            builder.HasIndex(u => u.CPF).IsUnique();

            builder.Property(u => u.Active);

            builder
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(u => u.Account)
                .WithOne()
                .HasForeignKey<User>(a => a.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
