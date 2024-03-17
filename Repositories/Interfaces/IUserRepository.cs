using ApolloBank.DTOs;
using ApolloBank.Models;

namespace ApolloBank.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDetailsDTO> CreateUser(CreateUserDTO createUserDTO); 
        Task<UserDetailsDTO> GetUserById(Guid id);
        Task<UserDetailsDTO> GetUserByEmail(string email);
        Task<UserDetailsDTO> GetUserByCPF(string cpf);
        Task<User> UpdateUser(UpdateUserDTO updateUserDTOser);
        Task<User> DeleteUser(Guid id);
        Task<List<UserDetailsDTO>> GetUsers();


    }



}
