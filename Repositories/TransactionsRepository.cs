﻿using ApolloBank.Data;
using ApolloBank.Enums;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace ApolloBank.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly AppDbContext _appDbContext;

        public TransactionsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Methods of adding transactions

        public Task<Transaction> AddScheduledTransaction(Transaction Transaction)
        {
            throw new NotImplementedException();
        }

        public async Task<Transaction> AddTransaction(Transaction transaction)
        {

            //Quem envia
            Account accountfrom = await GetTransactionByCode(Convert.ToInt32(transaction.From));
            if (accountfrom == null) throw new Exception("Source account holder not found.");
            //Para
            Account accountTo = await GetTransactionByCode(Convert.ToInt32(transaction.To));
            if (accountTo == null) throw new Exception("Destination account holder not found.");

            if (accountfrom == accountTo) throw new Exception("The destination account cannot be the same as the source account.");

            var fromTransaction = new Transaction(
                 amount: transaction.Amount,
                 to: transaction.To,
                 from: transaction.From,
                 date: transaction.Date,
                 description: transaction.Description,
                 transaction_Type: transaction.Transaction_Type,
                 direction: 'O', //"Outgoing" (Saída).
                 account_Id: accountfrom.Id
              );

            var toTransaction = new Transaction(
                 amount: transaction.Amount,
                 to: transaction.To,
                 from: transaction.From,
                 date: transaction.Date,
                 description: transaction.Description,
                 transaction_Type: transaction.Transaction_Type,
                 direction: 'I', // "Incoming" (Entrada)
                 account_Id: accountTo.Id
             );


            _appDbContext.Transactions.Add(fromTransaction);
            _appDbContext.Transactions.Add(toTransaction);



            using (var transactionn = _appDbContext.Database.BeginTransaction())
            {
                try
                {
                    string updateSql = $@"
                            UPDATE Accounts
                            SET Balance = CASE
                                WHEN AccountNumber = '{accountfrom.AccountNumber}' THEN Balance - {transaction.Amount}
                                WHEN AccountNumber = '{accountTo.AccountNumber}' THEN Balance + {transaction.Amount}
                                ELSE Balance
                            END
                            WHERE AccountNumber IN ('{accountfrom.AccountNumber}', '{accountTo.AccountNumber}');";

                    _appDbContext.Database.ExecuteSqlRaw(updateSql);

                    transactionn.Commit();
                }
                catch (Exception ex)
                {
                    transactionn.Rollback();
                    // Lidar com a exceção, se necessário
                }
            }
            await _appDbContext.SaveChangesAsync();

            return transaction;
        }
        #endregion

        #region Methods of search
        public async Task<IEnumerable<Transaction>> GetAllTransactions(int? id)
        {
            return await _appDbContext.Transactions.Where(x => x.Account_Id == id).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetCurrentMonthTransactions(int? id)
        {
            // Obtém a data atual
            DateTime currentDate = DateTime.Now;

            // Obtém o primeiro dia do mês atual
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            // Obtém o primeiro dia do próximo mês
            DateTime firstDayOfNextMonth = firstDayOfMonth.AddMonths(1);

            // Filtra as transações para o mês atual para a conta especificada
            return await _appDbContext.Transactions
                .Where(x => x.Account_Id == id && x.Date >= firstDayOfMonth && x.Date < firstDayOfNextMonth)
                .ToListAsync();
        }


        public async Task<IEnumerable<Transaction>> GetLastSixMonthsTransactions(int? id)
        {
            // Obtém a data atual
            DateTime currentDate = DateTime.Now;

            // Calcula o primeiro dia do mês atual
            DateTime firstDayOfCurrentMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            // Calcula o primeiro dia do mês seis meses atrás
            DateTime firstDayOfSixMonthsAgo = firstDayOfCurrentMonth.AddMonths(-6);

            // Filtra as transações para os últimos seis meses para a conta especificada
            return await _appDbContext.Transactions
                .Where(x => x.Account_Id == id && x.Date >= firstDayOfSixMonthsAgo && x.Date < firstDayOfCurrentMonth)
                .ToListAsync();
        }
        #endregion

        public async Task<Account> GetTransactionByCode(int code)
        {
            return await _appDbContext.Accounts.SingleOrDefaultAsync(x => x.AccountNumber == code);

        }


    }
}
