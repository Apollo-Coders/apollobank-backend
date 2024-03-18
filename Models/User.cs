using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Models
{
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
        public int DDD { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime BirthDay { get; set; }

        [Required]
        public string CPF { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;

        public virtual Address Address { get; set; } = new Address();
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
    }
}
