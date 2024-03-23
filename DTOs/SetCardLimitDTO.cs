using System.ComponentModel.DataAnnotations;

namespace ApolloBank.DTOs
{
    public class SetCardLimitDTO
    {
        [Required]
        public double NewLimit { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        public string CardNum { get; set; }
    }
}
