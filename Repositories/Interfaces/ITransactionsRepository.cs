using System.Transactions;

namespace ApolloBank.Repositories.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactions(int? id);
        Task<Transaction> GetLastSixMonthsTransactions(int? id);
        Task<Transaction> GetCurrentMonthTransactions(int? id);
        Task<Transaction> AddTransaction(Transaction Transactionto, Transaction TransactionFrom = null);
    }
}
