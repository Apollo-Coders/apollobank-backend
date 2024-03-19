using System.ComponentModel.DataAnnotations;

namespace ApolloBank.DTOs
{
    public class CreateUserDTO : BaseUserDTO
    {
        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(6, ErrorMessage = "Password must have 6 digits.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Password must have only numbers.")]
        public string Password { get; set; } = null!;
    }
}
