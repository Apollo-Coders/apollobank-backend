using ApolloBank.Data;
using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using ApolloBank.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApolloBank.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        private readonly HashService _hashService;
        private readonly IMapper _mapper;
        private readonly RandomNumberService _randomNumberService;

        public UserRepository(
            AppDbContext appDbContext,
            HashService hashService,
            IMapper mapper,
            RandomNumberService randomNumberService
        )
        {
            _appDbContext = appDbContext;
            _hashService = hashService;
            _mapper = mapper;
            _randomNumberService = randomNumberService;
        }

        public async Task<UserDetailsDTO> CreateUser(CreateUserDTO createUserDTO)
        {
            var existingEmail = await _appDbContext.Users.AnyAsync(
                u => u.Email == createUserDTO.Email
            );
            if (existingEmail)
            {
                throw new ArgumentException("O email já está em uso.");
            }

            var existingCPF = await _appDbContext.Users.AnyAsync(u => u.CPF == createUserDTO.CPF);
            if (existingCPF)
            {
                throw new ArgumentException("O CPF já está em uso.");
            }
            User user = _mapper.Map<User>(createUserDTO);
            string hashedPassword = _hashService.HashPassword(createUserDTO.Password);

            var account = new Account { AccountNumber = GenerateRandomAccountNumber(), UserId = user.Id, Balance = 3000 };
            var createdAccount = await _appDbContext.Accounts.AddAsync(account);
            await _appDbContext.SaveChangesAsync();

            user.Account = createdAccount.Entity;
            user.AccountId = createdAccount.Entity.Id;
            user.Password = hashedPassword;

            var createdUser = await _appDbContext.Users.AddAsync(user);

            var creditCards = new CreditCards();
            creditCards.TotalCreditUsed = 0.0d;
            creditCards.TotalCreditLimit = 5000;
            creditCards.TotalAlocatedCredit = 0.0d;
            creditCards.AccountId = createdUser.Entity.AccountId;

            Console.WriteLine(creditCards.AccountId);

            await _appDbContext.CreditCards.AddAsync(creditCards);
            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<UserDetailsDTO>(user);           
        }

        private int GenerateRandomAccountNumber(int length = 6)
        {
            var random = new Random();
            string number = random.Next(1, 10).ToString();
            number += _randomNumberService.GenerateRandomNumber(5).ToString();

            return int.Parse(number);
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _appDbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            user.Active = false;

            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<UserDetailsDTO> GetUserByCPF(string cpf)
        {
            var user = await _appDbContext
                .Users.Include(u => u.Address)
                .Include(u => u.Account)
                .FirstOrDefaultAsync(u => u.CPF == cpf);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return _mapper.Map<UserDetailsDTO>(user);
        }

        public async Task<UserDetailsDTO> GetUserByEmail(string email)
        {
            var user = await _appDbContext
                .Users.Include(u => u.Address)
                .Include(u => u.Account)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return _mapper.Map<UserDetailsDTO>(user);
        }

        public async Task<UserDetailsDTO> GetUserById(Guid id)
        {
            var user = await _appDbContext
                .Users.Include(u => u.Address)
                .Include(u => u.Account)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return _mapper.Map<UserDetailsDTO>(user);
        }

        public async Task<List<UserDetailsDTO>> GetUsers()
        {
            var users = await _appDbContext
                .Users.Include(u => u.Address)
                .Include(u => u.Account)
                .ToListAsync();

            return _mapper.Map<List<UserDetailsDTO>>(users);
        }

        public async Task<UserDetailsDTO> UpdateUser(Guid id, UpdateUserDTO updateUserDTO)
        {
            var existingUser = await _appDbContext
                .Users.Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id == id);

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
            if (
                existingUser.CPF != updateUserDTO.CPF
                && await _appDbContext.Users.AnyAsync(u => u.CPF == updateUserDTO.CPF)
            )
            {
                throw new ArgumentException("CPF already in use by another user");
            }

            _mapper.Map(updateUserDTO, existingUser);
            existingUser.Password = _hashService.HashPassword(updateUserDTO.Password);
            existingUser.UpdatedAt = DateTime.Now;

            _appDbContext.Users.Update(existingUser);
            await _appDbContext.SaveChangesAsync();

            var userDetailsDTO = _mapper.Map<UserDetailsDTO>(existingUser);

            return userDetailsDTO;
        }
    }
}
