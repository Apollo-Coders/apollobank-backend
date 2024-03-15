using ApolloBank.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApolloBank.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
       
        [Required(ErrorMessage = "The Amount is Required")]
        public double Amount { get; set; }
        
        public string? To { get; set; }
        
        public string? From { get; set; }
        
        public DateTime Date { get;  set; }
        
        
        [MinLength(5)]
        [MaxLength(200)]
        [DisplayName("Description")]
        public string? Description { get; set; }
        
        public TransactionType Transaction_Type { get; set; }
        public char Direction { get; set; }

        public int Account_Id { get; set; }
    }
}
