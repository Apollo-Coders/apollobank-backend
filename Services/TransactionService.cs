using ApolloBank.DTOs;
using ApolloBank.Repositories.Interfaces;
using ApolloBank.Services.Interfaces;

namespace ApolloBank.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionService(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }

        public Task<TransactionDTO> AddTransaction(TransactionDTO TransactionTo, TransactionDTO? TransactionFrom)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TransactionDTO>> GetAllTransactions(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionDTO> GetCurrentMonthTransactions(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionDTO> GetLastSixMonthsTransactions(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
