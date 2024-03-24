using ApolloBank.DTOs;

namespace ApolloBank.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAllTransactions(int? id);
        Task<TransactionDTO> GetTransaction(int? transaction_id, int? account_id);
        Task<IEnumerable<TransactionDTO>> GetLastSixMonthsTransactions(int? id);
        Task<IEnumerable<TransactionDTO>> GetCurrentMonthTransactions(int? id);
        
        
        Task<TransactionDTO> AddTransaction(TransactionDTO transactiondto);
        Task<TransactionDTO> Makewithdrawal(TransactionDTO transactiondto);
        Task<TransactionDTO> Makedeposit(TransactionDTO transactiondto);



        Task<TransactionDTO> Scheduletransaction(TransactionDTO transactionDto);
        Task<TransactionDTO> AddTransactionCredit(TransactionDTO transactionDto);
    }
}
