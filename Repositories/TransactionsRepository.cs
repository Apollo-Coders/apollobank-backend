using ApolloBank.Repositories.Interfaces;
using System.Transactions;

namespace ApolloBank.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        public Task<Transaction> AddTransaction(Transaction Transactionto, Transaction TransactionFrom = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetAllTransactions(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> GetCurrentMonthTransactions(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> GetLastSixMonthsTransactions(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
