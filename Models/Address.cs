using System.ComponentModel.DataAnnotations;

namespace ApolloBank.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; } = null!;
        public int Number { get; set; }
        public string? Complement { get; set; }
        public string Neighborhood { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public bool Policies { get; set; }

        public User? User { get; set; }
    }
}
