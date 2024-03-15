using ApolloBank.DTOs;

namespace ApolloBank.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAllTransactions(int? id);
        Task<IEnumerable<TransactionDTO>> GetLastSixMonthsTransactions(int? id);
        Task<IEnumerable<TransactionDTO>> GetCurrentMonthTransactions(int? id);
        Task AddTransaction(TransactionDTO Transaction);
    }
}
