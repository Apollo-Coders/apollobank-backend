using System.ComponentModel.DataAnnotations;

namespace ApolloBank.Models
{
    public class Account
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public int AccountNumber { get; set; }
        public CreditCards? CreditCards { get; set; }
        public List<CreditCard>? CreditCard { get; set; }

        public List<Transaction>? Transactions { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
