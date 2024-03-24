using ApolloBank.DTOs;
using ApolloBank.Models;

namespace ApolloBank.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> FoundExistingUser(string cpf);
        Task<bool> AuthenticateAsync(string cpf, string senha);
        public string GenerateToken(User user);
        public Task<User> FoundUserByCpf(string cpf);
        public TokenReturnDTO ResponseTokenData(string token, string fullName, double balance, int accountNumber, int accountId);
        public TokenInfoDTO GetTokenDateByHtppContext(HttpContext httpContext);
    }
}
