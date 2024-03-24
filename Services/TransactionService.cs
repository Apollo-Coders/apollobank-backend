using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Repositories;
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
        public async Task<TransactionDTO> AddTransaction(TransactionDTO transactiondto)
        {
            var transaction = _mapper.Map<Transaction>(transactiondto);
            var transactionResult = await _transactionsRepository.AddTransaction(transaction);
            var transactionResultDTO = _mapper.Map<TransactionDTO>(transactionResult);

            return transactionResultDTO;
        }

        public async Task<TransactionDTO> Makedeposit(TransactionDTO transactiondto)
        {
            var transaction = _mapper.Map<Transaction>(transactiondto);
            var transactionResult = await _transactionsRepository.Makedeposit(transaction);
            var transactionResultDTO = _mapper.Map<TransactionDTO>(transactionResult);


            return transactionResultDTO;
        }

        public async Task<TransactionDTO> Makewithdrawal(TransactionDTO transactiondto)
        {
            var transaction = _mapper.Map<Transaction>(transactiondto);
            var transactionResult = await _transactionsRepository.Makewithdrawal(transaction);
            var transactionResultDTO = _mapper.Map<TransactionDTO>(transactionResult);

            return transactionResultDTO;
        }

        public async Task<TransactionDTO> Scheduletransaction(TransactionDTO transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            var transactionResult = await _transactionsRepository.Scheduletransaction(transaction);
            var transactionResultDTO = _mapper.Map<TransactionDTO>(transactionResult);

            return transactionResultDTO;
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

        public async Task<TransactionDTO> GetTransaction(int? transaction_id, int? account_id)
        {
            var transaction = await  _transactionsRepository.GetTransaction(transaction_id, account_id);
            return _mapper.Map<TransactionDTO>(transaction);
        }

        #endregion

        public async Task<TransactionDTO> AddTransactionCredit(TransactionDTO transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            var transactionResult = await _transactionsRepository.AddTransactionCredit(transaction);
            var transactionResultDTO = _mapper.Map<TransactionDTO>(transactionResult);

            return transactionResultDTO;
        }
    }
}
