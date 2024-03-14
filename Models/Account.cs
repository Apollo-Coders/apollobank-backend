using System.ComponentModel.DataAnnotations;

namespace ApolloBank.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int Balance { get; set; }
        public string AccountNumber { get; set; } = null!;
        public CreditCards? CreditCards { get; set; }
        public Transaction? Transaction { get; set; }

        public float CreditLimit { get; set; }
        public List<CreditCard>? CreditCard { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
