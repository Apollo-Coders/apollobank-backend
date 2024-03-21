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



        #region Internal methods
        private async Task updateIncomingValue(double amount, int accountNumber)
        {
            Account account = await _accountRepository.GetAccountByAccountNumber(accountNumber);

            account.Balance += amount;
            _appDbContext.Accounts.Update(account);
            await _appDbContext.SaveChangesAsync();
        }
        private async Task updateOutgoingValue(double amount, int accountNumbe)
        {
            Account account = await _accountRepository.GetAccountByAccountNumber(accountNumbe);

            account.Balance -= amount;
            _appDbContext.Accounts.Update(account);
            await _appDbContext.SaveChangesAsync();
        }
        private async Task<(Account, Account)> ValidatingTransaction(Transaction transaction)
        {

            if (transaction.From == null) throw new Exception("O campo do titular da conta de origem não pode estar vazio");
            if (transaction.To == null) throw new Exception("O campo do titular da conta de destino não pode estar vazio.");
            Account accountFrom = await _accountRepository.GetAccountByAccountNumber(Convert.ToInt32(transaction.From));
            if (accountFrom == null) throw new Exception("Titular da conta de origem não encontrado para o número da conta fornecido.");
            if (accountFrom.Balance < transaction.Amount) throw new Exception("Saldo insuficiente.");
            Account accountTo = await _accountRepository.GetAccountByAccountNumber(Convert.ToInt32(transaction.To));
            if (accountTo == null) throw new Exception("Titular da conta de destino não encontrado para o número da conta fornecido.");
            if (accountFrom == accountTo) throw new Exception("A conta de destino não pode ser a mesma que a conta de origem.");
            return (accountFrom, accountTo);
        }
        #endregion

        #region Methods of adding transactions
        public async Task<Transaction> Makedeposit(Transaction transaction)
        {
            if (transaction.From == null) throw new Exception("O campo do titular da conta de destino não pode estar vazio.");
            Account account = await _accountRepository.GetAccountByAccountNumber(Convert.ToInt32(transaction.From));
            if (account == null) throw new Exception("Titular da conta não encontrado para o número da conta fornecido.");
            if (account.Id != transaction.Account_Id) throw new Exception("O ID da conta na transação não corresponde ao ID da conta atual.");

            var fromTransaction = new Transaction(
              amount: transaction.Amount,
              from: transaction.From,
              date: transaction.Date,
              description: transaction.Description,
              transaction_Type: transaction.Transaction_Type,
              direction: 'I', // "Incoming" (Entrada)
              account_Id: account.Id
             );
            using (var transactionn = await _appDbContext.Database.BeginTransactionAsync())

            {
                try
                {
                    _appDbContext.Transactions.Add(fromTransaction);
                    await updateIncomingValue(transaction.Amount, account.AccountNumber);

                    await transactionn.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transactionn.RollbackAsync();
                    throw new Exception("Ocorreu um erro ao processar a transação  de depósito.'", ex);
                }
            }
            await _appDbContext.SaveChangesAsync();

            return transaction;

        }
        public async Task<Transaction> Makewithdrawal(Transaction transaction)
        {
            if (transaction.From == null) throw new Exception("O campo do titular da conta de origem não pode estar vazio.");
            Account account = await _accountRepository.GetAccountByAccountNumber(Convert.ToInt32(transaction.From));
            if (account == null) throw new Exception("Titular da conta não encontrado para o número da conta fornecido.");
            if(account.Id != transaction.Account_Id) throw new Exception("O ID da conta na transação não corresponde ao ID da conta atual.");
            if (account.Balance < transaction.Amount) throw new Exception("Saldo insuficiente.");

                var fromTransaction = new Transaction(
               amount: transaction.Amount,
               from: transaction.From,
               date: transaction.Date,
               description: transaction.Description,
               transaction_Type: transaction.Transaction_Type,
               direction: 'O', //"Outgoing" (Saída).
               account_Id: account.Id
              );
       

            using (var transactionn = await _appDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _appDbContext.Transactions.Add(fromTransaction);

                    await updateOutgoingValue(transaction.Amount, account.AccountNumber);
                    
                    await transactionn.CommitAsync();
                }
                catch (Exception ex)
                {
                   await transactionn.RollbackAsync();
                    throw new Exception("Ocorreu um erro ao processar a transação  de saque.'", ex);
                }
            }
            await _appDbContext.SaveChangesAsync();

            return transaction;
        }
        public async Task<Transaction> AddTransaction(Transaction transaction)
        {

            var (accountfrom, accountTo) = await ValidatingTransaction(transaction);
           
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


            using (var transactionn = await _appDbContext.Database.BeginTransactionAsync())
            {
                try
                {

                    _appDbContext.Transactions.Add(fromTransaction);
                    _appDbContext.Transactions.Add(toTransaction);

                    await updateIncomingValue(transaction.Amount, accountTo.AccountNumber);
                    await updateOutgoingValue(transaction.Amount, accountfrom.AccountNumber);


                    await transactionn.CommitAsync();
                }
                catch (Exception ex)
                {
                    transactionn.Rollback();
                    throw new Exception("Ocorreu um erro ao processar a transação.'", ex);
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
            var Scheduledtransactions = await _appDbContext.Transactions
            .Where(t => t.ScheduledDate != null && ((DateTime)t.ScheduledDate).Date == today && t.TransactionStatusChecker == "Inprogress")
            .ToListAsync();

            return Scheduledtransactions;
        }
        public async Task<Transaction> Scheduletransaction(Transaction transaction)
        {

            var (accountfrom, accountTo) = await ValidatingTransaction(transaction);


            var fromTransaction = new Transaction(
                 amount: transaction.Amount,
                 to: Convert.ToString(accountTo.AccountNumber),
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
            await _appDbContext.SaveChangesAsync();

            return transaction;
        }
        public async Task<bool> CompleteScheduledTransaction(int? id)
        {
            if (id == null) throw new Exception("Transaction not found");
            Transaction transaction = await _appDbContext.Transactions.SingleAsync(x => x.Id == id);
            if (transaction == null) throw new Exception("Transaction not found");
          
            
            Account accountfrom = await _accountRepository.GetAccountByAccountNumber(Convert.ToInt32(transaction.From));
            Account accountTo = await _accountRepository.GetAccountByAccountNumber(Convert.ToInt32(transaction.To));

            transaction.TransactionStatusChecker = "Complete";
         


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

     


            using (var transactionn = await _appDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _appDbContext.Transactions.Update(transaction);

                    _appDbContext.Transactions.Add(toTransaction);

                    await updateIncomingValue(transaction.Amount, accountTo.AccountNumber);
                    await updateOutgoingValue(transaction.Amount, accountfrom.AccountNumber);


                    await transactionn.CommitAsync();
                }
                catch (Exception ex)
                {
                    transactionn.Rollback();
                    throw new Exception("Ocorreu um erro ao processar a transação.'", ex);
                }
            }

            await _appDbContext.SaveChangesAsync();

            return true;
        }
        #endregion




    }
}
