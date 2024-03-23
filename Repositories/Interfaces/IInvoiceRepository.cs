using ApolloBank.Models;
using System.Runtime.CompilerServices;

namespace ApolloBank.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        public Task<Invoice> GetActualMonthInvoice(int accountId);
        public Task<Invoice> GetMonthInvoice(int accountId, DateTime monthInvoiceDate);
        public Task<IEnumerable<Invoice>> GetAllInvoices(int accountId);
        public Task<Invoice> CreateMonthInvoice(int accountId);
        public Task AddAmountToInvoice(double amount, int accountId);
        public Task<Invoice> PayParcialMonthInvoice(string cardId, DateTime monthInvoice);
        public Task<Invoice> PayTotalMonthInvoice(int accountId, DateTime monthInvoice);
    }
}
