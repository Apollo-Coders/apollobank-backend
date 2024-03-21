using System.ComponentModel.DataAnnotations;

namespace ApolloBank.DTOs
{
    public class CreditCardDetailsDTO
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }
        public string Number { get; set; }
        public int Cvc { get; set; }
        public DateTime ExpirationTime { get; set; }
        public double CreditUsed { get; set; }
        public double CreditLimit { get; set; }
    }
}
