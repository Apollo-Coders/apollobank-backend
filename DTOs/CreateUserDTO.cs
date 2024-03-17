
using System.ComponentModel.DataAnnotations;


namespace ApolloBank.DTOs  
{
    public class CreateUserDTO:BaseUserDTO
    {
        [Required(ErrorMessage = "O campo 'Senha' é obrigatório.")]
        [MinLength(6, ErrorMessage = "O campo 'Senha' deve ter no mínimo 6 caracteres.")]
        public string Password { get; set; } = null!;
        public int AccountNumber { get; set; }
    }
}

