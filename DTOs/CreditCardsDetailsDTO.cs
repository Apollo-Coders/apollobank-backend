using ApolloBank.Models;

namespace ApolloBank.DTOs
{
    public class CreditCardsDetailsDTO
    {
        public int Id { get; set; }
        public double TotalCreditLimit { get; set; }
        public double TotalCreditUsed { get; set; }
        public double TotalAlocatedCredit { get; set; }
    }
}