using ApolloBank.DTOs;

namespace ApolloBank.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAllTransactions(int? id);
        Task<TransactionDTO> GetLastSixMonthsTransactions(int? id);
        Task<TransactionDTO> GetCurrentMonthTransactions(int? id);
        Task<TransactionDTO> AddTransaction(TransactionDTO TransactionTo, TransactionDTO? TransactionFrom);
    }
}
