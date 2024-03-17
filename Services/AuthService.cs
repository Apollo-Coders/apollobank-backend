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
        private readonly UserManager<IdentityUser> _userManager;
        private AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext appDbContext, IConfiguration configuration){
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        public bool AuthenticateAsync(string email, string senha)
        {
            var user = _appDbContext.Users.Where(u => u.Email.ToUpper() == email.ToUpper() && u.Password == senha).FirstOrDefaultAsync();
            if(user == null){
                return false;
            }
            return true;
        }//refatorar isso, ele retorna true sempre.

        public async Task<bool> FoundExistingUser(string cpf)
        {
            var user = await _appDbContext.Users.Where(u => u.CPF == cpf).FirstOrDefaultAsync();
            if(user != null)
            {
                return true;
            }
            return false;
        }

        public string GenerateToken([FromBody] User user)
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
    }
}