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




        #region Methods that receive a DTO object and convert it into an entity
        public async Task AddTransaction(TransactionDTO transactiondto)
        {
            var transaction = _mapper.Map<Transaction>(transactiondto);
            await _transactionsRepository.AddTransaction(transaction);
        }

        public async Task Makedeposit(TransactionDTO transactiondto)
        {
            var transaction = _mapper.Map<Transaction>(transactiondto);
            await _transactionsRepository.Makedeposit(transaction);
        }

        public async Task Makewithdrawal(TransactionDTO transactiondto)
        {
            var transaction = _mapper.Map<Transaction>(transactiondto);
            await _transactionsRepository.Makewithdrawal(transaction);
        }

        public async Task Scheduletransaction(TransactionDTO transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            await _transactionsRepository.Scheduletransaction(transaction);
        }

        public async Task<bool> CompleteScheduledTransaction(int? id)
        {
           
            return await _transactionsRepository.CompleteScheduledTransaction(id);
        }
        #endregion


        #region Methods that receive an entity and convert it into a DTO object
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

        public async Task<IEnumerable<TransactionDTO>> GetScheduledTransaction()
        {
            var transaction = await _transactionsRepository.GetScheduledTransaction();
            return _mapper.Map<IEnumerable<TransactionDTO>>(transaction);
        }

    


        #endregion
    }
}
