using ApolloBank.Models;
using Microsoft.EntityFrameworkCore;

namespace ApolloBank.Data
{
    public class ApolloBankContext : DbContext
    {
        public ApolloBankContext(DbContextOptions<ApolloBankContext> options)
            : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Account> Adress { get; set; }
    }
}
