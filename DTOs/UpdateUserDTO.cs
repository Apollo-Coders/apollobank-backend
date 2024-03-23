using System.ComponentModel.DataAnnotations;

namespace ApolloBank.DTOs
{
    public class UpdateUserDTO : BaseUserDTO
    {


        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must have at least 6 digits.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Password must have only numbers.")]
        public string Password { get; set; } = null!;
    }
}
