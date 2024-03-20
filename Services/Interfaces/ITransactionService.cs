using ApolloBank.DTOs;

namespace ApolloBank.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAllTransactions(int? id);
        Task<IEnumerable<TransactionDTO>> GetLastSixMonthsTransactions(int? id);
        Task<IEnumerable<TransactionDTO>> GetCurrentMonthTransactions(int? id);
        
        
        Task AddTransaction(TransactionDTO transactiondto);
        Task Makewithdrawal(TransactionDTO transactiondto);
        Task Makedeposit(TransactionDTO transactiondto);


        Task<List<TransactionDTO>> GetScheduledTransaction();
        Task Scheduletransaction(TransactionDTO transactionDto);
        Task<bool> CompleteScheduledTransaction(int? id);
    }
}
