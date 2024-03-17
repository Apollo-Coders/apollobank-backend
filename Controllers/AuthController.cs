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
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository, IAuthService authService){
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Authenticate([FromBody] UserRequestDTO data)
        {
            if (data == null)
            {
                return BadRequest("Não foi possível completar o login pois existem parâmetros nulos.");
            }

            var existsUser = await _authService.FoundExistingUser(data.cpf);

            if(!existsUser)
            {
                return Unauthorized("Usuário não existe.");
            }
            
            bool AuthenticateAsync = _authService.AuthenticateAsync(data.cpf, data.password);
            if(AuthenticateAsync)
            {
                User user = await _userRepository.GetUserByCPF(data.cpf);
                //adicionar account id
                var token = _authService.GenerateToken(user);
                return Ok(token);
            }
            return BadRequest("Credenciais incorretas.");
        }
    }
}