using ApolloBank.Data;
using ApolloBank.Enums;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApolloBank.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICreditCardsRepository _creditCardsRepository;

        public InvoiceRepository(AppDbContext appDbContext, ICreditCardsRepository creditCardsRepository)
        {
            _appDbContext = appDbContext;
            _creditCardsRepository = creditCardsRepository;
        }


        // Retorna a fatura do mês atual
        public async Task<Invoice> GetActualMonthInvoice(int accountId)
        {
            // Pegar a data do primeiro dia do mês atual no horário zerado
            DateTime actualMonthDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            var actualMonthInvoice = await _appDbContext.Invoices.FirstOrDefaultAsync(i => i.InvoiceDate == actualMonthDate && i.AccountId == accountId);

            if (actualMonthInvoice == null)
            {
                var invoice = await CreateMonthInvoice(accountId);
                return invoice;
            }

            return actualMonthInvoice;
        }


        // Retorna a fatura de um mês em específico
        public async Task<Invoice> GetMonthInvoice(int accountId, DateTime monthInvoiceDate)
        {
            // Pegar a data do primeiro dia do mês e ano do monthInvoiceDate 
            DateTime monthDate = new DateTime(monthInvoiceDate.Year, monthInvoiceDate.Month, 1);

            var monthInvoice = await _appDbContext.Invoices.FirstOrDefaultAsync(i => i.InvoiceDate == monthDate && i.AccountId == accountId);

            if (monthInvoice == null)
            {
                if(monthInvoiceDate.Month == DateTime.Now.Month && monthInvoiceDate.Year == DateTime.Now.Year)
                {
                    var invoice = await CreateMonthInvoice(accountId);
                    return invoice;
                }
                throw new Exception("Fatura do mês informado não encontrada");
            }

            return monthInvoice;
        }


        // Retorna todas as faturas dos outros meses
        public async Task<IEnumerable<Invoice>> GetAllInvoices(int accountId)
        {
            var allInvoice = await _appDbContext.Invoices.Where(i => i.AccountId == accountId).ToListAsync();

            return allInvoice;
        }


        // Cria a fatura do mês atual (todo dia 1 ésse método executa para todas as contas)
        public async Task<Invoice> CreateMonthInvoice(int accountId)
        {
            // Pegar a data do primeiro dia do mês atual no horário zerado
            DateTime actualMonthDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            double invoiceTotalAmount = 0.0d;
            double invoicePaid = 0.0d;
            InvoiceStatus invoiceStatus = InvoiceStatus.PENDING;
            var monthInvoice = new Invoice(actualMonthDate, invoiceTotalAmount, invoicePaid, invoiceStatus, accountId);

            try
            {
                var returnedInvoice = await _appDbContext.Invoices.AddAsync(monthInvoice);
                await _appDbContext.SaveChangesAsync();

                return returnedInvoice.Entity;
            } 
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar invoice do mês", ex);
            }
        }


        // Adiciona um valor passado no cartão na fatura do mês
        public async Task AddAmountToInvoice(double amount, int accountId)
        {
            var actualMonthInvoice = await GetActualMonthInvoice(accountId);

            actualMonthInvoice.InvoiceTotalAmount += amount;

            _appDbContext.Invoices.Update(actualMonthInvoice);
            await _appDbContext.SaveChangesAsync();
        }


        // Paga pacialmente uma fatura (paga um cartão)
        public async Task<Invoice> PayParcialMonthInvoice(string cardNum, DateTime monthInvoiceDate)
        {
            var creditCard = await _creditCardsRepository.GetCardByCardNumber(cardNum);

            int accountId = creditCard.AccountId ?? -1;
            double amountToPay = creditCard.CreditUsed;

            var invoiceToPay = await GetMonthInvoice(accountId, monthInvoiceDate);

            using (var transaction = await _appDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    invoiceToPay.InvoicePaid -= amountToPay;
                    await _creditCardsRepository.PayCreditCard(accountId, cardNum);
                    _appDbContext.Invoices.Update(invoiceToPay);

                    await _appDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    
                    return invoiceToPay;

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Houve um erro interno ao alterar o limite no cartão", ex);
                }

            }

        }
        

        // Pagar a fatura total de uma vez (paga todos os cartões)
        public async Task<Invoice> PayTotalMonthInvoice(int accountId, DateTime monthInvoiceDate)
        {
            var creditCardList = await _creditCardsRepository.GetAllCardByAccountId(accountId);

           

            var invoiceToPay = await GetMonthInvoice(accountId, monthInvoiceDate);

            using (var transaction = await _appDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    invoiceToPay.InvoicePaid = 0;

                    foreach (var creditCard in creditCardList)
                    {
                        string cardNum = creditCard.Number;                        
                        await _creditCardsRepository.PayCreditCard(accountId, cardNum);

                        await _appDbContext.SaveChangesAsync();
                    }

                    _appDbContext.Invoices.Update(invoiceToPay);
                    await transaction.CommitAsync();
                    return invoiceToPay;

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Houve um erro interno ao alterar o limite no cartão", ex);
                }

            }

        }

    }
}
