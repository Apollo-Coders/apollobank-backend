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
        public DateTime? ScheduledDate { get; private set; }
        public string? Description { get; private set; }
        public TransactionType Transaction_Type { get; private set; }
        public char Direction { get; private set; }
        public int? Account_Id { get; set; }
        public Account? Account { get; set; }

        public Transaction()
        { }

        public Transaction(int id, double amount, string? to, string? from, DateTime date, DateTime? scheduledDate, string? description, TransactionType transaction_Type, char direction, int? account_Id, Account? account)
        {
            Id = id;
            Amount = amount;
            To = to;
            From = from;
            Date = date;
            ScheduledDate = scheduledDate;
            Description = description;
            Transaction_Type = transaction_Type;
            Direction = direction;
            Account_Id = account_Id;
            Account = account;
        }

        public Transaction( double amount, string? to, string? from, DateTime date, string? description, TransactionType transaction_Type, char direction, int? account_Id)
        {
            Amount = amount;
            To = to;
            From = from;
            Date = date;
            Description = description;
            Transaction_Type = transaction_Type;
            Direction = direction;
            Account_Id = account_Id;
        }

        //OBS: INCLUIR UM CONTRUTOR SEM A DATA DE AGENDAMENTO
    }
}
