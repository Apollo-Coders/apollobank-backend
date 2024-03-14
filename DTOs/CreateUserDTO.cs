using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApolloBank.Enums;
using ApolloBank.Models;

namespace ApolloBank.DTOs
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        [MinLength(3, ErrorMessage = "O campo 'Nome' deve ter no mínimo 3 caracteres.")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "O campo 'Email' é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo 'Email' deve ser um email válido.")]
        public string Email { get; set; } = null!;

        public int DDD { get; set; }

        [Phone(
            ErrorMessage = "O campo 'número de telefone' deve ser um número de telefone válido."
        )]
        public int PhoneNumber { get; set; }
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "O campo 'CPF' é obrigatório.")]
        [StringLength(11, ErrorMessage = "O campo 'CPF' deve ter 11 caracteres.")]
        public string CPF { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;
        public int? AddressId { get; set; }
        public virtual Address? Address { get; set; }
        public int AccountId { get; set; }
        
    }

  
}
