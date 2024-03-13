using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;


namespace ApolloBank.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        public Task<Transaction> AddTransaction(Transaction TransactionTo, Transaction? TransactionFrom)
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
