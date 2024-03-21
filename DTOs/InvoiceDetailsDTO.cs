using ApolloBank.Enums;
using ApolloBank.Models;

namespace ApolloBank.DTOs
{
    public class InvoiceDetailsDTO
    {
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double InvoiceTotalAmount { get; set; }
        public double InvoicePaid { get; set; }
        public InvoiceStatus Status { get; set; }
    }
}
