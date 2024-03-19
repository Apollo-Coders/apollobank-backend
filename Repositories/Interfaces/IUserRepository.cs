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
        Task<UserDetailsDTO> UpdateUser(Guid id, UpdateUserDTO updateUserDTOser);
        Task<bool> DeleteUser(Guid id);
        Task<List<UserDetailsDTO>> GetUsers();


    }



}
