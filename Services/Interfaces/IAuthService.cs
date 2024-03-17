using ApolloBank.DTOs;
using ApolloBank.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> FoundExistingUser(string cpf);
        bool AuthenticateAsync(string email, string senha);
        public string GenerateToken(User user);
        
    }
}
