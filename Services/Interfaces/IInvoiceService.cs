﻿using ApolloBank.DTOs;

namespace ApolloBank.Services.Interfaces
{
    public interface IInvoiceService
    {
        public Task<InvoiceDetailsDTO> GetActualMonthInvoice(int accountId);
        public Task<InvoiceDetailsDTO> GetMonthInvoice(int accountId, DateTime monthInvoiceDate);
        public Task<IEnumerable<InvoiceDetailsDTO>> GetAllInvoices(int accountId);
        public Task<InvoiceDetailsDTO> CreateMonthInvoice(int accountId);
        public Task<InvoiceDetailsDTO> PayParcialMonthInvoice(string cardNum, DateTime monthInvoiceDate);
        public Task<InvoiceDetailsDTO> PayTotalMonthInvoice(int accountId, DateTime monthInvoiceDate);
    }
}
