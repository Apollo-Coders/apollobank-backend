﻿using ApolloBank.Models;

namespace ApolloBank.Repositories.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactions(int? id);
        Task<IEnumerable<Transaction>> GetLastSixMonthsTransactions(int? id);
        Task<IEnumerable<Transaction>> GetCurrentMonthTransactions(int? id);
        
        
        Task<Transaction> AddTransaction(Transaction transaction);

        //Fazer saque
        Task<Transaction> Makewithdrawal(Transaction transaction);

        //Fazer deposito

        Task<Transaction> Makedeposit(Transaction transaction);
        
        Task<IEnumerable<Transaction>> GetScheduledTransaction();
        Task<Transaction> Scheduletransaction(Transaction transaction);
        Task<bool> CompleteScheduledTransaction(int? id);
   
        





    }
}
