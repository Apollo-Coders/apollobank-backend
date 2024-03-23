using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ApolloBank.Data;
using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ApolloBank.Services
{

    public class AuthService : IAuthService
    {
        private AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        private IHashService _hashService;

        public AuthService(AppDbContext appDbContext, IConfiguration configuration, IHashService hashService)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
            _hashService = hashService;
        }

        public async Task<bool> AuthenticateAsync(string cpf, string senha)
        {
            string hashPassword = _hashService.HashPassword(senha);

            User? user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.CPF == cpf && u.Password == hashPassword);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> FoundExistingUser(string cpf)
        {
            var user = await _appDbContext.Users.Where(u => u.CPF == cpf).FirstOrDefaultAsync();
            if (user != null)
            {
                return true;
            }
            return false;

        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("accountID", user.AccountId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.
            GetBytes(_configuration["jwt:secretKey"]));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(15);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> FoundUserByCpf(string cpf)
        {
            User? user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.CPF == cpf);
            if (user == null)
            {
                throw new Exception("CPF não cadastrado");
            }
            return user;
        }

        public TokenReturnDTO ResponseTokenData(string token, string userName, double balance, int accountNumber)
        {
            TokenReturnDTO responseUserData = new()
            {
                token = token,
                userName = userName,
                balance = balance,
                accountNumber = accountNumber
            };
            return responseUserData;
        }
    }

}