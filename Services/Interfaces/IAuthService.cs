using ApolloBank.DTOs;
using ApolloBank.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> FoundExistingUser(string cpf);
        Task<bool> AuthenticateAsync(string cpf, string senha);
        public string GenerateToken(User user);
        public Task<User> FoundUserByCpf(string cpf);
        TokenReturnDTO responseTokenData(string token, string fullName, double balance, int accountNumber);
    }
}
