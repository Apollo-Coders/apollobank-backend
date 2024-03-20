using System.ComponentModel.DataAnnotations;

namespace ApolloBank.DTOs
{
    public class CreditCardDetailsDTO
    {

        [Key]
        public int Id { get; set; }
        public bool IsBlocked { get; set; }
        public string Number { get; set; }

        [Range(100, 999)]
        public int Cvc { get; set; }

        [DataType(DataType.Date)]
        public DateTime ExpirationTime { get; set; }
        public double CreditUsed { get; set; }
        public double CreditLimit { get; set; }
        public int AccountId { get; set; }
    }
}
