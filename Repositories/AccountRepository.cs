using ApolloBank.Data;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

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
            return null;
        }

        public Task<Account> GetAccountByAccountNumber(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetAccountByAccountNumber(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Account> GetAccountByUserId(Guid id)
        {
            var account = await _appDbContext.Accounts.FirstOrDefaultAsync(a => a.User.Id == id);
            if (account == null)
            {
                throw new Exception("Erro ao encontrar a conta");
            }

            return account;
        }

    }
}
