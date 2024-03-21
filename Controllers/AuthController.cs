using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Repositories;
using ApolloBank.Repositories.Interfaces;
using ApolloBank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository, IAuthService authService)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpPost("auth/login")]
        public async Task<ActionResult> Authenticate([FromBody] UserRequestDTO data)
        {
            if (data == null)
            {
                return BadRequest("Não foi possível completar o login pois existem parâmetros nulos.");
            }

            var existsUser = await _authService.FoundExistingUser(data.cpf);

            if (!existsUser)
            {
                return BadRequest("Usuário não encontrado");
            }

            bool isAuthenticated = await _authService.AuthenticateAsync(data.cpf, data.password);
            if (isAuthenticated)
            {
                User user = await _authService.FoundUserByCpf(data.cpf);
                var token = _authService.GenerateToken(user);

                TokenReturnDTO response = _authService.responseTokenData(token, user.FullName, 300.00, 300);
                if (response != null)
                {
                    return Ok(response);
                }
                return BadRequest("Valores nulos");
            }
            return Unauthorized("Credenciais inválidas.");
        }
    }
}