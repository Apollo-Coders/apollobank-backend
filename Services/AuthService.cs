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
                new Claim("accountId", user.AccountId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.
            GetBytes(_configuration["jwt:secretKey"]!));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //var expiration = DateTime.UtcNow.AddMinutes(15);
            var expiration = DateTime.UtcNow.AddMinutes(15).AddDays(2);

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

        public TokenReturnDTO ResponseTokenData(string token, string userName, double balance, int accountNumber, int accountId)
        {
            TokenReturnDTO responseUserData = new()
            {
                token = token,
                userName = userName,
                balance = balance,
                accountNumber = accountNumber,
                accountId = accountId
            };
            return responseUserData;
        }

        public TokenInfoDTO GetTokenDateByHtppContext(HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last() ?? throw new Exception("Erro na requisição");

            // Cria um JwtSecurityTokenHandler para ler o token JWT
            var handler = new JwtSecurityTokenHandler();

            // Lê o token JWT
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            // Obtém as reivindicações (claims) do token
            var claims = jwtToken!.Claims;

            // Converte as reivindicações para um dicionário para facilitar o acesso aos dados
            var tokenInfo = new TokenInfoDTO();

            tokenInfo.UserId = Guid.Parse(claims.FirstOrDefault(c => c.Type == "id")?.Value!);
            tokenInfo.Email = claims.FirstOrDefault(c => c.Type == "email")?.Value!;
            string accoutId = claims.FirstOrDefault(c => c.Type == "accountId")?.Value!;
            tokenInfo.AccountId = int.Parse(accoutId);

            return tokenInfo;

        }
    }


}