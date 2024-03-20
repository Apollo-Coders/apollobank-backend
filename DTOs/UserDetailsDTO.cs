using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApolloBank.DTOs
{
    public class UserDetailsDTO:BaseUserDTO
    {
         [Key]
        public Guid Id { get;set; }
        public int AccountNumber { get; set; }
        
    }
}