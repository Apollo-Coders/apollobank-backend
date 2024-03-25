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

            if (account == null)
            {
                throw new Exception("Conta não encontrada");
            }
            return account;
        }
        public async Task<Account> GetAccountByAccountId(int id)
        {
            var account = await _appDbContext.Accounts.FirstOrDefaultAsync(
              a => a.Id == id);

            if (account == null)
            {
                throw new Exception("Conta não encontrada");
            }
            return account;
        }

        
     

        public async Task<Account> GetAccountByUserId(Guid id)
        {
            var account = await _appDbContext.Accounts.FirstOrDefaultAsync(a => a.User.Id == id);

            if (account == null)
            {
                throw new Exception("Conta não encontrada");
            }
            return account;
        }

    }
}
