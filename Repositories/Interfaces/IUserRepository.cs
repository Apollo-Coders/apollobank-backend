using ApolloBank.Models;

namespace ApolloBank.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByCPF(string cpf);
        Task<User> CreateUser(User user); //Vai receber um DTO
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(int id);

    }
}
