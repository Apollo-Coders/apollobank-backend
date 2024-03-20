using ApolloBank.Data;
using ApolloBank.Migrations;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ApolloBank.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAccountRepository _accountRepository;

        public TransactionsRepository(AppDbContext appDbContext, IAccountRepository accountRepository)
        {
            _appDbContext = appDbContext;
            _accountRepository = accountRepository;
        }



        #region Methods of adding transactions
        public async Task<Transaction> Makedeposit(Transaction transaction)
        {
            Account account = await _accountRepository.GetAccountByAccountNumber(Convert.ToInt32(transaction.From));
            if (account == null) throw new Exception("Source account holder not found.");


            _appDbContext.Transactions.Add(transaction);

            using (var transactionn = _appDbContext.Database.BeginTransaction())
            {
                try
                {
                    string updateSql = $@"
                            UPDATE Accounts
                            SET Balance = CASE
                                WHEN AccountNumber = '{account.AccountNumber}' THEN Balance + {transaction.Amount}
                                ELSE Balance
                            END
                            WHERE AccountNumber IN ('{account.AccountNumber}');";

                    _appDbContext.Database.ExecuteSqlRaw(updateSql);

                    transactionn.Commit();
                }
                catch (Exception ex)
                {
                    transactionn.Rollback();
                    throw new Exception("An error occurred while processing the transaction.'");
                }
            }
            await _appDbContext.SaveChangesAsync();

            return transaction;

        }
        public async Task<Transaction> Makewithdrawal(Transaction transaction)
        {
            Account account = await _accountRepository.GetAccountByAccountNumber(Convert.ToInt32(transaction.From));
            if (account == null) throw new Exception("Source account holder not found.");


            _appDbContext.Transactions.Add(transaction);

            using (var transactionn = _appDbContext.Database.BeginTransaction())
            {
                try
                {
                    string updateSql = $@"
                            UPDATE Accounts
                            SET Balance = CASE
                                WHEN AccountNumber = '{account.AccountNumber}' THEN Balance - {transaction.Amount}
                                ELSE Balance
                            END
                            WHERE AccountNumber IN ('{account.AccountNumber}');";

                    _appDbContext.Database.ExecuteSqlRaw(updateSql);

                    transactionn.Commit();
                }
                catch (Exception ex)
                {
                    transactionn.Rollback();
                    throw new Exception("An error occurred while processing the transaction.'");
                }
            }
            await _appDbContext.SaveChangesAsync();

            return transaction;
        }
        public async Task<Transaction> AddTransaction(Transaction transaction)
        {


            Account accountfrom = await _accountRepository.GetAccountByAccountNumber(Convert.ToInt32(transaction.From));
            if (accountfrom == null) throw new Exception("Source account holder not found.");

            Account accountTo = await _accountRepository.GetAccountByAccountNumber(Convert.ToInt32(transaction.To));
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
                    throw new Exception("An error occurred while processing the transaction.'");
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

            DateTime currentDate = DateTime.Now;
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime firstDayOfNextMonth = firstDayOfMonth.AddMonths(1);


            return await _appDbContext.Transactions
                .Where(x => x.Account_Id == id && x.Date >= firstDayOfMonth && x.Date < firstDayOfNextMonth)
                .ToListAsync();
        }
        public async Task<IEnumerable<Transaction>> GetLastSixMonthsTransactions(int? id)
        {

            DateTime currentDate = DateTime.Now;
            DateTime firstDayOfCurrentMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime firstDayOfSixMonthsAgo = firstDayOfCurrentMonth.AddMonths(-6);
            return await _appDbContext.Transactions
                .Where(x => x.Account_Id == id && x.Date >= firstDayOfSixMonthsAgo && x.Date < firstDayOfCurrentMonth)
                .ToListAsync();
        }
        #endregion

        #region Methods for scheduled transaction
        public async Task<List<Transaction>> GetScheduledTransaction()
        {

            DateTime today = DateTime.Now.Date;
            var teste = await _appDbContext.Transactions
            .Where(t => t.ScheduledDate != null && ((DateTime)t.ScheduledDate).Date == today && t.TransactionStatusChecker == "Inprogress")
            .ToListAsync();

            return teste;
        }
        public async Task<Transaction> Scheduletransaction(Transaction transaction)
        {

            Account accountfrom = await _accountRepository.GetAccountByAccountNumber(Convert.ToInt32(transaction.From));
            if (accountfrom == null) throw new Exception("Source account holder not found.");
            Account accountTo = await _accountRepository.GetAccountByAccountNumber(Convert.ToInt32(transaction.To));
            if (accountTo == null) throw new Exception("Destination account holder not found.");
            if (accountfrom == accountTo) throw new Exception("The destination account cannot be the same as the source account.");


            var fromTransaction = new Transaction(
                 amount: transaction.Amount,
                 to: transaction.To,
                 from: transaction.From,
                 date: transaction.Date,
                 scheduledDate: transaction.ScheduledDate,
                 transactionStatusChecker: "Inprogress",
                 description: transaction.Description,
                 transaction_Type: transaction.Transaction_Type,
                 direction: 'O', //"Outgoing" (Saída).
                 account_Id: accountfrom.Id
              );

            _appDbContext.Transactions.Add(fromTransaction);


            return transaction;
        }
        public async Task<bool> CompleteScheduledTransaction(int? id)
        {
            if (id == null) throw new Exception("Transaction not found");


            Transaction transaction = await _appDbContext.Transactions.SingleAsync(x => x.Id == id);

            if (transaction == null) throw new Exception("Transaction not found");
            Account accountfrom = await _accountRepository.GetAccountByAccountNumber(Convert.ToInt32(transaction.From));
            if (accountfrom == null) throw new Exception("Source account holder not found.");
            Account accountTo = await _accountRepository.GetAccountByAccountNumber(Convert.ToInt32(transaction.To));
            if (accountTo == null) throw new Exception("Destination account holder not found.");
            if (accountfrom == accountTo) throw new Exception("The destination account cannot be the same as the source account.");

            transaction.TransactionStatusChecker = "Complete";
            _appDbContext.Transactions.Update(transaction);


            var toTransaction = new Transaction(
                 amount: transaction.Amount,
                 to: transaction.To,
                 from: transaction.From,
                 date: transaction.Date,
                  scheduledDate: transaction.ScheduledDate,
                 transactionStatusChecker: "Complete",
                 description: transaction.Description,
                 transaction_Type: transaction.Transaction_Type,
                 direction: 'I',
                 account_Id: accountTo.Id
             );

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
                    throw new Exception("An error occurred while processing the transaction.'");
                }
            }

            await _appDbContext.SaveChangesAsync();

            return true;
        }
        #endregion
    }
}
