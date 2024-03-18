using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApolloBank.DTOs
{
    public class UserDetailsDTO:BaseUserDTO
    {
         public int AccountNumber { get; set; }
    }
}