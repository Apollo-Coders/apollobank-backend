using ApolloBank.Data;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;


namespace ApolloBank.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private AppDbContext _appDbContext;

        public TransactionsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

     

        public Task<IEnumerable<Transaction>> GetAllTransactions(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetCurrentMonthTransactions(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetLastSixMonthsTransactions(int? id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Transaction>> ITransactionsRepository.AddTransaction(Transaction TransactionTo, Transaction TransactionFrom)
        {
            throw new NotImplementedException();
        }
    }
}
