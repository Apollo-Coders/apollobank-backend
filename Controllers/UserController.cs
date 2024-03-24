using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using ApolloBank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IAccountRepository _accountRepository; 

        public UserController(IUserRepository userRepository, IAuthService authService, IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _authService = authService;
            _accountRepository = accountRepository; 
        }

        /// <summary>
        /// Endpoint utilizado para criar um novo usuário.
        /// </summary>
        /// <param name="createUserDTO">Dados necessários para criar um novo usuário.</param>
        /// <returns>Retorna um objeto ActionResult contendo um CreateUserDTO em caso de sucesso, ou uma mensagem de erro em caso de falha na criação do usuário.</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<CreateUserDTO>> CreateUser(CreateUserDTO createUserDTO)
        {
            try
            {
                var user = await _userRepository.CreateUser(createUserDTO);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message }); //modificando retorno pra voltar um objeto
            }
        }


        /// <summary>
        /// Endpoint utilizado para obter detalhes de um usuário pelo seu ID.
        /// </summary>
        /// <returns>Retorna um objeto ActionResult contendo os detalhes do usuário em caso de sucesso, ou uma mensagem de erro em caso de usuário não encontrado.</returns>
        [HttpGet()]
        public async Task<ActionResult<UserDetailsDTO>> GetUser()
        {
            Guid id = _authService.GetTokenDateByHtppContext(HttpContext).UserId;

            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }



        /// <summary>
        /// Endpoint utilizado para atualizar informações de um usuário autenticado.
        /// </summary>
        /// <param name="updateUserDTO">Dados necessários para atualizar as informações do usuário.</param>
        /// <returns>Retorna um objeto ActionResult contendo os detalhes atualizados do usuário em caso de sucesso, ou uma mensagem de erro em caso de falha na atualização.</returns>
        [Authorize]
        [HttpPut()]
        public async Task<ActionResult<UserDetailsDTO>> UpdateUser(
            UpdateUserDTO updateUserDTO
        )
        {
            Guid id = _authService.GetTokenDateByHtppContext(HttpContext).UserId;

            try
            {
                var updatedUser = await _userRepository.UpdateUser(id, updateUserDTO);
                if (updatedUser == null)
                {
                    return NotFound("User not found.");
                }
                return CreatedAtAction(nameof(GetUser), new { id = updatedUser.Id }, updatedUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint utilizado para excluir um usuário autenticado.
        /// </summary>
        /// <returns>Retorna um objeto ActionResult indicando o resultado da operação de exclusão. Em caso de sucesso, retorna um status 200 OK com uma mensagem indicando que o usuário foi excluído com sucesso. Em caso de falha, retorna um status 404 Not Found com uma mensagem indicando que o usuário não foi encontrado.</returns>
        [Authorize]
        [HttpDelete()]
        public async Task<ActionResult> DeleteUser()
        {
            Guid id = _authService.GetTokenDateByHtppContext(HttpContext).UserId;

            var user = await _userRepository.DeleteUser(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok("User deleted successfully");
        }


        /// <summary>
        /// Endpoint utilizado para obter detalhes de um usuário pelo seu endereço de e-mail.
        /// </summary>
        /// <param name="email">O endereço de e-mail do usuário.</param>
        /// <returns>Retorna um objeto ActionResult contendo os detalhes do usuário em caso de sucesso, ou uma mensagem de erro em caso de usuário não encontrado.</returns>
        [Authorize]
        [HttpGet()]
        public async Task<ActionResult<UserDetailsDTO>> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }


        /// <summary>
        /// Endpoint utilizado para obter detalhes de um usuário pelo seu CPF.
        /// </summary>
        /// <param name="cpf">O número de CPF do usuário.</param>
        /// <returns>Retorna um objeto ActionResult contendo os detalhes do usuário em caso de sucesso, ou uma mensagem de erro em caso de usuário não encontrado.</returns>
        [Authorize]
        [HttpGet("{cpf}")]
        public async Task<ActionResult<UserDetailsDTO>> GetUserByCPF(string cpf)
        {
            var user = await _userRepository.GetUserByCPF(cpf);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        /// <summary>
        /// Endpoint utilizado para obter detalhes de todos os usuários.
        /// </summary>
        /// <returns>Retorna um objeto ActionResult contendo os detalhes de todos os usuários em caso de sucesso.</returns>
        [Authorize]
        [HttpGet("GetUsers")]
        public async Task<ActionResult<UserDetailsDTO>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            return Ok(users);
        }

        /// <summary>
        /// Endpoint utilizado para obter informações de uma conta pelo seu número de identificação.
        /// </summary>
        /// <param name="id">O número de identificação da conta.</param>
        /// <returns>Retorna um objeto ActionResult contendo as informações da conta em caso de sucesso, ou um status NotFound em caso de conta não encontrada.</returns>
        [Authorize]
        [HttpGet("GetAccount/{id}")]

        public async Task <ActionResult<Account>> GetAccountInformation(int id)
        {
            var account = await _accountRepository.GetAccountByAccountNumber(id); 
            return Ok(account);

        }


    }
}
