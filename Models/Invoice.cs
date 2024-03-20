using ApolloBank.Enums;

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

        public Invoice(int id, DateTime invoiceDate, double invoiceTotalAmount, double invoicePaid){
            Id = id;
            InvoiceDate = invoiceDate;
            InvoiceTotalAmount = invoiceTotalAmount;
            InvoicePaid = invoicePaid;
        }

       
    }
}
