using ApolloBank.Data;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApolloBank.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _appDbContext;

        public InvoiceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Invoice> CreateMonthInvoice(int accountId) 
        {
            // Pegar a data do primeiro dia do mês atual no horário zerado
            DateTime actualMonthDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            var monthInvoice = new Invoice(actualMonthDate, accountId);

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

        public async Task AddAmountToInvoice(double amount, int accountId)
        {
            var actualMonthInvoice = await GetActualMonthInvoice(accountId);

            actualMonthInvoice.InvoiceTotalAmount += amount;

            _appDbContext.Invoices.Update(actualMonthInvoice);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Invoice> GetActualMonthInvoice(int accountId)
        {
            // Pegar a data do primeiro dia do mês atual no horário zerado
            DateTime actualMonthDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            var actualMonthInvoice = await _appDbContext.Invoices.FirstOrDefaultAsync(i => i.InvoiceDate == actualMonthDate && i.AccountId == accountId);

            if (actualMonthInvoice == null)
            {
                throw new Exception("Fatura do mês atual não encontrada");
            }

            return actualMonthInvoice;
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoices(int accountId)
        {
            var allInvoice = await _appDbContext.Invoices.Where(i => i.AccountId == accountId).ToListAsync();

            return allInvoice;
        }
    }
}
