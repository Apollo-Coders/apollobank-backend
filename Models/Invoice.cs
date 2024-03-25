using ApolloBank.Enums;

namespace ApolloBank.Models
{
    public class Invoice
    {
        public int? Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double InvoiceTotalAmount { get; set; }
        public double InvoicePaid { get; set; }

        public InvoiceStatus Status { get; set; }

        public int? AccountId { get; set; }
        public Account? Account { get; set; }

        public Invoice() { }
        public Invoice(DateTime invoiceDate, double invoiceTotalAmount, double invoicePaid, InvoiceStatus status, int accountId){
            InvoiceDate = invoiceDate;
            InvoiceTotalAmount = invoiceTotalAmount;
            InvoicePaid = invoicePaid;
            Status = status;
            AccountId = accountId;
        }

       
    }
}
