using ApolloBank.Models;

namespace ApolloBank.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        public Task<Invoice> CreateMonthInvoice(int accountId);
        public Task AddAmountToInvoice(double amount, int accountId);

        public Task<Invoice> GetActualMonthInvoice(int accountId);

        public Task<IEnumerable<Invoice>> GetAllInvoices(int accountId); 
    }
}
