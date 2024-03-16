using ApolloBank.Data;
using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using ApolloBank.Services;
using Microsoft.EntityFrameworkCore;

namespace ApolloBank.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        private readonly HashService _hashService;

        public UserRepository(AppDbContext appDbContext, HashService hashService)
        {
            _appDbContext = appDbContext;
            _hashService = hashService;
        }

        public async Task<User> CreateUser(CreateUserDTO createUserDTO)
        {
            var existingEmail = await _appDbContext.Users.AnyAsync(
                u => u.Email == createUserDTO.Email
            );
            if (existingEmail)
            {
                throw new ArgumentException("Email already in use.");
            }

            var existingCPF = await _appDbContext.Users.AnyAsync(u => u.CPF == createUserDTO.CPF);
            if (existingCPF)
            {
                throw new ArgumentException("CPF already in use.");
            }
            string hashedPassword = _hashService.HashPassword(createUserDTO.Password);
            var user = new User
            {
                FullName = createUserDTO.FullName,
                Email = createUserDTO.Email,
                Password = hashedPassword,
                DDD = createUserDTO.DDD,
                PhoneNumber = createUserDTO.PhoneNumber,
                BirthDay = createUserDTO.BirthDay,
                CPF = createUserDTO.CPF,
                Active = createUserDTO.Active,
                Address = new Address
                {
                    Street = createUserDTO.Street,
                    Number = createUserDTO.Number,
                    Complement = createUserDTO.Complement,
                    Neighborhood = createUserDTO.Neighborhood,
                    City = createUserDTO.City,
                    State = createUserDTO.State
                },
                Account = new Account { AccountNumber = GenerateRandomAccountNumber() }
            };         


            var createdUser = await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();

            return createdUser.Entity;
        }

        private int GenerateRandomAccountNumber(int length = 6)
        {
            var random = new Random();
            string number = "";
            for (int i = 0; i < length; i++)
            {
                number += random.Next(0, 10).ToString();
            }
            return int.Parse(number);
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await _appDbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUserByCPF(string cpf)
        {
            var user = await _appDbContext.Users.FindAsync(cpf);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _appDbContext.Users.FindAsync(email);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _appDbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        public async Task<User> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            var existingUser = await _appDbContext
                .Users.Include(u => u.Address) 
                .FirstOrDefaultAsync(u => u.Id == Guid.Parse(updateUserDTO.Id.ToString()));

            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (
                existingUser.Email != updateUserDTO.Email
                && await _appDbContext.Users.AnyAsync(u => u.Email == updateUserDTO.Email)
            )
            {
                throw new ArgumentException("Email already in use by another user");
            }

            existingUser.FullName = updateUserDTO.FullName;
            existingUser.Email = updateUserDTO.Email;
            existingUser.DDD = updateUserDTO.DDD;
            existingUser.Password = _hashService.HashPassword(updateUserDTO.Password);
            existingUser.PhoneNumber = updateUserDTO.PhoneNumber;
            existingUser.BirthDay = updateUserDTO.BirthDay;
            existingUser.CPF = updateUserDTO.CPF;
            existingUser.UpdatedAt = DateTime.Now;
            existingUser.Active = updateUserDTO.Active;

            if (existingUser.Address != null)
            {
                existingUser.Address.Street = updateUserDTO.Street;
                existingUser.Address.Number = updateUserDTO.Number.ToString();
                existingUser.Address.Complement = updateUserDTO.Complement;
                existingUser.Address.Neighborhood = updateUserDTO.Neighborhood;
                existingUser.Address.City = updateUserDTO.City;
                existingUser.Address.State = updateUserDTO.State;
            }
            else
            {
                existingUser.Address = new Address
                {
                    Street = updateUserDTO.Street,
                    Number = updateUserDTO.Number.ToString(),
                    Complement = updateUserDTO.Complement,
                    Neighborhood = updateUserDTO.Neighborhood,
                    City = updateUserDTO.City,
                    State = updateUserDTO.State
                };
            }

            _appDbContext.Users.Update(existingUser);
            await _appDbContext.SaveChangesAsync();

            return existingUser;
        }
    }
}
