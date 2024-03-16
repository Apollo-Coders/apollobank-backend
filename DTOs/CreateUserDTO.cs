
using System.ComponentModel.DataAnnotations;


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

        [Required(ErrorMessage = "O campo 'Senha' é obrigatório.")]
        [MinLength(6, ErrorMessage = "O campo 'Senha' deve ter no mínimo 6 caracteres.")]
        public string Password { get; set; } = null!;

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
        public int AccountId { get; set; }
    }
}

