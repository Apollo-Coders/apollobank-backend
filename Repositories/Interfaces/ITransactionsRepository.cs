using ApolloBank.Models;

namespace ApolloBank.Repositories.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactions(int? id);
        Task<IEnumerable<Transaction>> GetLastSixMonthsTransactions(int? id);
        Task<IEnumerable<Transaction>> GetCurrentMonthTransactions(int? id);
        Task<IEnumerable<Transaction>> AddTransaction(Transaction TransactionTo, Transaction TransactionFrom);
    }
}
