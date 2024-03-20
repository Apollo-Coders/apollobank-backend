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

        public int? Account_Id { get; private set; }
        public Account? Account { get; private set; }

        
    }
}
