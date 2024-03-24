
using ApolloBank.Models;

namespace ApolloBank.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByAccountNumber(int id);
        Task<Account> GetAccountByAccountId(int id);
        Task<Account> GetAccountByUserId(Guid id);
    }
}