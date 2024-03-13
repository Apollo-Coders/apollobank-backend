using ApolloBank.Enums;
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
        
        public string? Description { get; set; }
        
        public TransactionType Transaction_Type { get; set; }
        public double Direction { get; set; }

        public int Account_Id { get; set; }
    }
}
