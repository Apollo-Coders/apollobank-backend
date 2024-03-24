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
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICreditCardsRepository _creditCardsRepository;

        public TransactionsRepository(AppDbContext appDbContext, IAccountRepository accountRepository, IInvoiceRepository invoiceRepository, ICreditCardsRepository creditCardsRepository)
        {
            _appDbContext = appDbContext;
            _accountRepository = accountRepository;
            _invoiceRepository = invoiceRepository;
            _creditCardsRepository = creditCardsRepository;
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
            if (account.Id != transaction.AccountId) throw new Exception("O ID da conta na transação não corresponde ao ID da conta atual.");

            var fromTransaction = new Transaction(
              amount: transaction.Amount,
              from: transaction.From,
              date: transaction.Date,
              description: transaction.Description,
              transactionType: transaction.TransactionType,
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

            return fromTransaction;

        }
        public async Task<Transaction> Makewithdrawal(Transaction transaction)
        {
            if (transaction.From == null) throw new Exception("O campo do titular da conta de origem não pode estar vazio.");
            Account account = await _accountRepository.GetAccountByAccountNumber(Convert.ToInt32(transaction.From));
            if (account == null) throw new Exception("Titular da conta não encontrado para o número da conta fornecido.");
            if(account.Id != transaction.AccountId) throw new Exception("O ID da conta na transação não corresponde ao ID da conta atual.");
            if (account.Balance < transaction.Amount) throw new Exception("Saldo insuficiente.");

                var fromTransaction = new Transaction(
               amount: transaction.Amount,
               from: transaction.From,
               date: transaction.Date,
               description: transaction.Description,
               transactionType: transaction.TransactionType,
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

            return fromTransaction;
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
                 transactionType: transaction.TransactionType,
                 direction: 'O', //"Outgoing" (Saída).
                 account_Id: accountfrom.Id
              );

            var toTransaction = new Transaction(
                 amount: transaction.Amount,
                 to: transaction.To,
                 from: transaction.From,
                 date: transaction.Date,
                 description: transaction.Description,
                 transactionType: transaction.TransactionType,
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

            return fromTransaction;
        }
        #endregion

        #region Methods of search
        public async Task<IEnumerable<Transaction>> GetAllTransactions(int? id)
        {
            return await _appDbContext.Transactions.Where(x => x.AccountId == id).ToListAsync();
        }
        public async Task<IEnumerable<Transaction>> GetCurrentMonthTransactions(int? id)
        {

            DateTime currentDate = DateTime.Now;
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime firstDayOfNextMonth = firstDayOfMonth.AddMonths(1);


            return await _appDbContext.Transactions
                .Where(x => x.AccountId == id && x.Date >= firstDayOfMonth && x.Date < firstDayOfNextMonth)
                .ToListAsync();
        }
        public async Task<IEnumerable<Transaction>> GetLastSixMonthsTransactions(int? id)
        {

            DateTime currentDate = DateTime.Now;
            DateTime sixMonthsAgo = currentDate.AddMonths(-6);

            var transactions = await _appDbContext.Transactions
                .Where(x => x.AccountId == id && x.Date >= sixMonthsAgo)
                .ToListAsync();

            return transactions;
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
                 transactionType: transaction.TransactionType,
                 direction: 'O', //"Outgoing" (Saída).
                 account_Id: accountfrom.Id
              );

            _appDbContext.Transactions.Add(fromTransaction);
            await _appDbContext.SaveChangesAsync();

            return fromTransaction;
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
                 transactionType: transaction.TransactionType,
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

        public async Task<Transaction> GetTransaction(int? transaction_id, int? account_id)
        {
            return await _appDbContext.Transactions
             .SingleAsync(x => x.Id == transaction_id && x.AccountId == account_id);
        }






        #endregion


        // Método que contém a lógica de adicionar uma o valor de uma transação no limite usado de crédito (será usado na transaction)
        public async Task AddAmountToUsedCredit(double amount, int accountId, string cardNum)
        {
            var creditCards = await _creditCardsRepository.GetCreditCardsByAccountId(accountId);
            var creditCard = await _creditCardsRepository.GetCardByCardNumber(cardNum);

            double availableLimit = _creditCardsRepository.VerifyCardLimit(creditCard);
            var invoice = await _invoiceRepository.GetActualMonthInvoice(accountId);

            if (invoice == null) await _invoiceRepository.CreateMonthInvoice(accountId);


            if (availableLimit < amount)
            {
                throw new Exception("Compra reprovada: Cartão não possui limite suficiente");
            }


            using (var transaction = await _appDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    creditCards.TotalCreditUsed += amount;
                    creditCard.CreditUsed += amount;

                    _appDbContext.CreditCards.Update(creditCards);
                    _appDbContext.CreditCard.Update(creditCard);
                    await _invoiceRepository.AddAmountToInvoice(amount, accountId);

                    await _appDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Houve um erro interno ao alterar o limite no cartão", ex);
                }

            }
        }

        public async Task<Transaction> AddTransactionCredit(Transaction transaction)
        {

            if (transaction.From == null) throw new Exception("O campo do numero do cartao do titular da conta de origem não pode estar vazio.");

            CreditCard card = await _creditCardsRepository.GetCardByCardNumber(transaction.From);
            Account account = await _accountRepository.GetAccountByAccountId(Convert.ToInt32(card.AccountId));

            double availableLimit = card.CreditLimit - card.CreditUsed;

            if (account == null) throw new Exception("Titular da conta não encontrado para o número da conta fornecido.");
            if (account.Id != transaction.AccountId) throw new Exception("O ID da conta na transação não corresponde ao ID da conta atual.");
            if (card.IsBlocked) throw new Exception("Compra recusada: Cartão bloqueado.");
            if (availableLimit < transaction.Amount) throw new Exception("Compra recusada: Crédito insuficiente");

            var transactionCreditCard = new Transaction(
                 amount: transaction.Amount,
                 from: transaction.From, //Numero do cartao de credito 
                 date: transaction.Date,
                 description: transaction.Description,
                 transactionType: transaction.TransactionType,
                 direction: 'I', // "Incoming" (Entrada)
                 account_Id: account.Id
             );


            using (var transactionn = await _appDbContext.Database.BeginTransactionAsync())
            {
                try
                {

                    _appDbContext.Transactions.Add(transactionCreditCard);




                    await transactionn.CommitAsync();
                }
                catch (Exception ex)
                {
                    transactionn.Rollback();
                    throw new Exception("Ocorreu um erro ao processar a transação.'", ex);
                }
            }

            await _appDbContext.SaveChangesAsync();

            await AddAmountToUsedCredit(transaction.Amount, account.Id, card.Number);
            return transactionCreditCard;




            throw new NotImplementedException();
        }

    }
}
