using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApolloBank.DTOs
{
    public class BaseUserDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name must have at least 3 characteres.")]
        [MaxLength(100, ErrorMessage = "Name must have at most 100 characteres.")]
        [RegularExpression(
            @"^[a-zA-Zçãõáéíóúàèìòùâêîôûäëïöüñ\s]*$",
            ErrorMessage = "Name must have only letters."
        )]

        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "The email address must be a valid email.")]
        []
        
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "DDD is required.")]
        [Range(11, 99, ErrorMessage = "DDD must have 2 digits.")]
        public int DDD { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Phone Number must have only numbers.")]
        public int PhoneNumber { get; set; }

        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "CPF is required.")]
        [StringLength(11, ErrorMessage = "CPF must have 11 digits.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "CPF must have only numbers.")]
        public string CPF { get; set; } = null!;

        public bool Active { get; set; } = true;

        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "Number is required.")]
        public string Number { get; set; } = null!;

        public string Complement { get; set; } = string.Empty;

        [Required(ErrorMessage = "Neighborhood is required.")]
        public string Neighborhood { get; set; } = null!;

        [Required(ErrorMessage = "City is required.")]
        [RegularExpression(@"^[a-zA-Zçãõáéíóúàèìòùâêîôûäëïöüñ\s]*$", ErrorMessage = "City must have only letters.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "State is required.")]
        [StringLength(2, ErrorMessage = "State must have 2 characters.")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "State must have only letters.")]
        public string State { get; set; } = null!;
    }
}
