
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace ApolloBank.DTOs
{
    public class BaseUserDTO
    {
        [Key]
        public Guid Id { get;set; }

        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        [MinLength(3, ErrorMessage = "O campo 'Nome' deve ter no mínimo 3 caracteres.")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "O campo 'Email' é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo 'Email' deve ser um email válido.")]
        public string Email { get; set; } = null!;        

        [Required(ErrorMessage = "O campo 'DDD' é obrigatório.")]
        public int DDD { get; set; }

        [Required(ErrorMessage = "O campo 'Número de telefone' é obrigatório.")]
        public int PhoneNumber { get; set; }
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "O campo 'CPF' é obrigatório.")]
        [StringLength(11, ErrorMessage = "O campo 'CPF' deve ter 11 caracteres.")]
        public string CPF { get; set; } = null!;

        public bool Active { get; set; } = true;

        [Required(ErrorMessage = "O campo 'Rua' é obrigatório.")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "O campo 'Número' é obrigatório.")]
        public string Number { get; set; }= null!;

        public string Complement { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo 'Bairro' é obrigatório.")]
        public string Neighborhood { get; set; } = null!;

        [Required(ErrorMessage = "O campo 'Cidade' é obrigatório.")]
        public string City { get; set; }= null!;

        [Required(ErrorMessage = "O campo 'Estado' é obrigatório.")]
        public string State { get; set; }= null!;
        
    }
}