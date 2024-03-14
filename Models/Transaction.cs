using ApolloBank.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApolloBank.Models
{
    public class Transaction
    {
        public int Id { get; private set; }
        public double Amount { get; private set; }
        public string? To { get; private set; }
        public string? From { get; private set; }
        public DateTime Date { get; private set; } = DateTime.Now;
        public string? Description { get; private set; }
        public TransactionType Transaction_Type { get; private set; }
        public double Direction { get; private set; }
        public int? Account_Id { get; set; }

        public Transaction(int id, double amount, string? to, string? from, DateTime date, string? description, TransactionType transaction_Type, double direction, int account_Id)
        {
            Id = id;
            Amount = amount;
            To = to;
            From = from;
            Date = date;
            Description = description;
            Transaction_Type = transaction_Type;
            Direction = direction;
            Account_Id = account_Id;
        }
    }



    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Amount).IsRequired().HasPrecision(10, 2); 
            builder.Property(u => u.Description).HasMaxLength(100).HasColumnType("varchar(100)");
            builder.Property(u => u.Transaction_Type).IsRequired();
        }
    }
}
