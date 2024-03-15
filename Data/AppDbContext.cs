using ApolloBank.MapConfig;
using ApolloBank.Models;
using Microsoft.EntityFrameworkCore;

namespace ApolloBank.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<CreditCards> CreditCards { get; set;}
        public DbSet<CreditCard> CreditCard { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("DataSource=app.db;Cache=shared");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new AccountMap());
            modelBuilder.ApplyConfiguration(new AddressMap());
            modelBuilder.ApplyConfiguration(new CreditCardMap());
            modelBuilder.ApplyConfiguration(new CreditCardsMap());
            modelBuilder.ApplyConfiguration(new InvoiceMap());
            modelBuilder.ApplyConfiguration(new TransactionMap());
        }

    }
}
