﻿using ApolloBank.Enums;

namespace ApolloBank.Models
{
    public class Invoice
    {
        public int? Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double InvoiceTotalAmount { get; set; }
        public double InvoicePaid { get; set; }

        public InvoiceStatus status { get; private set; }

        public int? AccountId { get; set; }
        public Account? Account { get; set; }

        Invoice() { }

        public Invoice(DateTime invoiceDate, int accountId){
            InvoiceDate = invoiceDate;
            InvoiceTotalAmount = 0.0d;
            InvoicePaid = 0.0d;
            AccountId = accountId;
        }

        
    }
}
