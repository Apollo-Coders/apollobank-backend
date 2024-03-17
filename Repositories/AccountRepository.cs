using ApolloBank.Data;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApolloBank.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _appDbContext;

        public AccountRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Account> GetAccountByAccountNumber(int accountNumber)
        {
            var account = await _appDbContext.Accounts.FirstOrDefaultAsync(
                a => a.AccountNumber == accountNumber
            );
            if (account != null)
            {
                return account;
            }
            else
            {
                return null;
            }
        }
    }
}
