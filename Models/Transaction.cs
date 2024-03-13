﻿namespace ApolloBank.Models
{
    public class Transaction
    {
        public int Id { get; private set; }
        public int Amount { get; private set; }
        public string? To { get; private set; }
        public string? From { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
        //Criar Enuns
        public string Transaction_Type { get; private set; }
        public int Account_Id { get; set; }

        public Transaction(int id, int amount, string? to, string? from, DateTime date, string description, string transaction_Type, int account_Id)
        {
            Id = id;
            Amount = amount;
            To = to;
            From = from;
            Date = date;
            Description = description;
            Transaction_Type = transaction_Type;
            Account_Id = account_Id;
        }
    }
}
