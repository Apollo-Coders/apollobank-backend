using System.ComponentModel.DataAnnotations;

namespace ApolloBank.DTOs
{
    public class UserRequestDTO
    {
        [Required(ErrorMessage = "O CPF deverá ser informado para a realização do Login.")]
        public string cpf { get; set; }

        [Required(ErrorMessage = "A senha deverá ser informada para a realização do login")]
        public string password { get; set; }
    }
}