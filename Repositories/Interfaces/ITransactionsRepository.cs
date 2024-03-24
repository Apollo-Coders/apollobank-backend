using ApolloBank.Models;

namespace ApolloBank.Repositories.Interfaces
{
    public interface ITransactionsRepository
    {

        Task<IEnumerable<Transaction>> GetAllTransactions(int? id);
        Task<Transaction> GetTransaction(int? transaction_id, int? account_id);
        Task<IEnumerable<Transaction>> GetLastSixMonthsTransactions(int? id);
        Task<IEnumerable<Transaction>> GetCurrentMonthTransactions(int? id);
        
        
        Task<Transaction> AddTransaction(Transaction transaction);
        Task<Transaction> Makewithdrawal(Transaction transaction);
        Task<Transaction> Makedeposit(Transaction transaction);
        

        Task<List<Transaction>> GetScheduledTransaction();
        Task<Transaction> Scheduletransaction(Transaction transaction);
        Task<bool> CompleteScheduledTransaction(int? id);


        public Task AddAmountToUsedCredit(double amount, int accountId, string cardNum);
        Task<Transaction> AddTransactionCredit(Transaction transaction);






    }
}
