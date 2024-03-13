using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using ApolloBank.Services.Interfaces;
using AutoMapper;


namespace ApolloBank.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionsRepository transactionsRepository, IMapper mapper)
        {
            _transactionsRepository = transactionsRepository;
            _mapper = mapper;
        }




        #region Métodos que recebem um objeto DTO e o convertem em uma entidade 
        public async Task AddTransaction(TransactionDTO TransactionTo, TransactionDTO TransactionFrom)
        {
            var transactionTo = _mapper.Map<Transaction>(TransactionTo);
            var transactionFrom = _mapper.Map<Transaction>(TransactionFrom);

            await _transactionsRepository.AddTransaction(transactionTo, transactionFrom);
        }
        #endregion


        #region Métodos que recebem uma entidade e a convertem em um objeto DTO
        public async Task<IEnumerable<TransactionDTO>> GetAllTransactions(int? id)
        {
            var transaction = await _transactionsRepository.GetAllTransactions(id);
            return _mapper.Map<IEnumerable<TransactionDTO>>(transaction);
        }

        public async Task<IEnumerable<TransactionDTO>> GetCurrentMonthTransactions(int? id)
        {
            var transaction = await _transactionsRepository.GetCurrentMonthTransactions(id);
            return _mapper.Map<IEnumerable<TransactionDTO>>(transaction);
        }

        public async Task<IEnumerable<TransactionDTO>> GetLastSixMonthsTransactions(int? id)
        {
            var transaction = await _transactionsRepository.GetLastSixMonthsTransactions(id);
            return _mapper.Map<IEnumerable<TransactionDTO>>(transaction);
        }
        #endregion
    }
}
