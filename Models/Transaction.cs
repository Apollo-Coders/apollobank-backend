using ApolloBank.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string? TransactionStatusChecker { get;  set; }
        public string? Description { get; private set; }

        
        public TransactionType TransactionType { get; private set; }
        public char Direction { get; private set; }
        public int? AccountId { get; set; }
        public Account? Account { get; set; }

        public Transaction()
        { }

        public Transaction(int id, double amount, string? to, string? from,DateTime date, DateTime? scheduledDate,string? transactionStatusChecker, string? description, TransactionType transactionType, char direction, int? account_Id, Account? account)
        {
            Id = id;
            Amount = amount;
            To = to;
            From = from;
            Date = date;
            ScheduledDate = scheduledDate;
            TransactionStatusChecker = transactionStatusChecker;
            Description = description;
            TransactionType = transactionType;
            Direction = direction;
            AccountId = account_Id;
            Account = account;
        }

        public Transaction( double amount, string? to, string? from, DateTime date, string? description, TransactionType transactionType, char direction, int? account_Id)
        {
            Amount = amount;
            To = to;
            From = from;
            Date = date;
            Description = description;
            TransactionType = transactionType;
            Direction = direction;
            AccountId = account_Id;
        }

        public Transaction(double amount, string? to, string? from, DateTime date, DateTime? scheduledDate, string? transactionStatusChecker, string? description, TransactionType transactionType, char direction, int? account_Id)
        {
            Amount = amount;
            To = to;
            From = from;
            Date = date;
            ScheduledDate = scheduledDate;
            TransactionStatusChecker = transactionStatusChecker;
            Description = description;
            TransactionType = transactionType;
            Direction = direction;
            AccountId = account_Id;
        }

        public Transaction(double amount, string? from, DateTime date, string? description, TransactionType transactionType, char direction, int? account_Id)
        {
            Amount = amount;
            From = from;
            Date = date;
            Description = description;
            TransactionType = transactionType;
            Direction = direction;
            AccountId = account_Id;
        }
    



        //OBS: INCLUIR UM CONTRUTOR SEM A DATA DE AGENDAMENTO
    }
}
