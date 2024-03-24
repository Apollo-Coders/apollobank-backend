using System.ComponentModel.DataAnnotations;

namespace ApolloBank.DTOs
{
    public class CreateUserDTO : BaseUserDTO
    {
        [Required(ErrorMessage = "Senha não informada.")]
        [MaxLength(6, ErrorMessage = "Senha precisa ter 6 digítos.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Senha deve possuir apenas números.")]
        public string Password { get; set; } = null!;
    }
}
