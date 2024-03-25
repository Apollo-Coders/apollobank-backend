using ApolloBank.DTOs;
using ApolloBank.Repositories.Interfaces;
using ApolloBank.Services.Interfaces;
using AutoMapper;

namespace ApolloBank.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task<InvoiceDetailsDTO> GetActualMonthInvoice(int accountId)
        {
            var actualMonthInvoice = await _invoiceRepository.GetActualMonthInvoice(accountId);
            return _mapper.Map<InvoiceDetailsDTO>(actualMonthInvoice);
        }

        public async Task<InvoiceDetailsDTO> GetMonthInvoice(int accountId, DateTime monthInvoiceDate)
        {
            var monthInvoice = await _invoiceRepository.GetMonthInvoice(accountId, monthInvoiceDate);
            return _mapper.Map<InvoiceDetailsDTO>(monthInvoice);
        }

        public async Task<IEnumerable<InvoiceDetailsDTO>> GetAllInvoices(int accountId)
        {
            var allInvoices = await _invoiceRepository.GetAllInvoices(accountId);
            return _mapper.Map<IEnumerable<InvoiceDetailsDTO>>(allInvoices);
        }

        public async Task<InvoiceDetailsDTO> CreateMonthInvoice(int accountId)
        {
            var newInvoice = await _invoiceRepository.CreateMonthInvoice(accountId);
            return _mapper.Map<InvoiceDetailsDTO>(newInvoice);
        }

        public async Task PayParcialMonthInvoice(string cardNum, DateTime monthInvoiceDate)
        {
            var paidInvoice = await _invoiceRepository.PayParcialMonthInvoice(cardNum, monthInvoiceDate);
        }

        public async Task PayTotalMonthInvoice(int accountId, DateTime monthInvoiceDate)
        {
            var paidInvoice = await _invoiceRepository.PayTotalMonthInvoice(accountId, monthInvoiceDate);
        }
    }
}
