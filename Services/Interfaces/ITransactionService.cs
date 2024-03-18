using ApolloBank.DTOs;

namespace ApolloBank.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAllTransactions(int? id);
        Task<IEnumerable<TransactionDTO>> GetLastSixMonthsTransactions(int? id);
        Task<IEnumerable<TransactionDTO>> GetCurrentMonthTransactions(int? id);
        Task AddTransaction(TransactionDTO transactiondto);

        //Fazer saque
        Task Makewithdrawal(TransactionDTO transactiondto);

        //Fazer deposito

        Task Makedeposit(TransactionDTO transactiondto);





        Task<IEnumerable<TransactionDTO>> GetScheduledTransaction();
        Task Scheduletransaction(TransactionDTO transactionDto);
        Task<bool> CompleteScheduledTransaction(int? id);
    }
}
