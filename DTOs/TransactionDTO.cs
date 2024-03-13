using ApolloBank.Enums;

namespace ApolloBank.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string? To { get; set; }
        public string? From { get; set; }
        public DateTime Date { get;  set; }
        public string? Description { get; set; }
        public TransactionType Transaction_Type { get; set; }
        public int Account_Id { get; set; }
    }
}
