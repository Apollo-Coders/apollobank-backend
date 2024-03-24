using ApolloBank.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApolloBank.DTOs
{
    public class TransactionDTO
    {
        public int? Id { get; set; }
       
        [Required(ErrorMessage = "Valor da transação não informado.")]
        public double Amount { get; set; }
        
        public string? To { get; set; }
        
        public string? From { get; set; }
        
        public DateTime Date { get;  set; }
        public DateTime? ScheduledDate { get; set; }
        public string? TransactionStatusChecker { get; set; }


        [MinLength(5)]
        [MaxLength(200)]
        [DisplayName("Description")]
        public string? Description { get; set; }
        
        public TransactionType TransactionType { get; set; }
        public char Direction { get; set; }

        public int? AccountId { get; set; }

        public TransactionDTO()
        {
        }
    }
}
