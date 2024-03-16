using ApolloBank.DTOs;
using ApolloBank.Models;

namespace ApolloBank.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUser(CreateUserDTO createUserDTO); 
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByCPF(string cpf);
        Task<User> UpdateUser(UpdateUserDTO updateUserDTOser);
        Task<User> DeleteUser(int id);
        Task<List<User>> GetUsers();


    }



}
