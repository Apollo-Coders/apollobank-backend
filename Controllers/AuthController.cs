using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using ApolloBank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;

        public AuthController(IUserRepository userRepository, IAuthService authService, IAccountRepository accountRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
            _accountRepository = accountRepository;
        }



        /// <summary>
        /// Endpoint utilizado para autenticar um usuário.
        /// </summary>
        /// <param name="data">Dados de entrada do usuário (CPF e senha).</param>
        /// <returns>Retorna um token de autenticação em caso de sucesso, ou mensagens de erro em caso de falha na autenticação.</returns>
        [AllowAnonymous]
        [HttpPost()]
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

                Account userAccount = await _accountRepository.GetAccountByUserId(user.Id);

                if (userAccount != null)
                {
                    TokenReturnDTO response = _authService.ResponseTokenData(token, user.FullName, userAccount.Balance, userAccount.AccountNumber, userAccount.Id);
                    if (response != null)
                    {
                        return Ok(response);
                    }
                    return BadRequest("Valores nulos");
                }
                return BadRequest("Os dados do usuário não foram encontrados." + token);
            }
            return Unauthorized("Credenciais inválidas.");
        }
    }
}