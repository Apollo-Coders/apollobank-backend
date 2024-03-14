using ApolloBank.Enums;

namespace ApolloBank.Models
{
    public class Invoice
    {
        public int Id { get; private set; }
        public DateTime InvoiceDate { get; private set; }
        public double InvoiceTotalAmount { get; private set; }
        public double InvoicePaid { get; private set; }

        public InvoiceStatus status { get; private set; }

        public Invoice(int id, DateTime invoiceDate, double invoiceTotalAmount, double invoicePaid){
            Id = id;
            InvoiceDate = invoiceDate;
            InvoiceTotalAmount = invoiceTotalAmount;
            InvoicePaid = invoicePaid;
        }

        public void payInvoice(double amount){
            InvoicePaid += amount;
            if(InvoicePaid >= InvoiceTotalAmount){
                status = InvoiceStatus.PAIDOUT;
            } else if(InvoicePaid < InvoiceTotalAmount && InvoiceDate > DateTime.Now){
                status = InvoiceStatus.PENDING;
            } else if(InvoicePaid < InvoiceTotalAmount && InvoiceDate < DateTime.Now){
                status = InvoiceStatus.OVERDUE;
            }
        }
    }
}
