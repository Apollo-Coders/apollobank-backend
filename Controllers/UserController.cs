using ApolloBank.DTOs;
using ApolloBank.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

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

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsDTO>> GetUser(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDetailsDTO>> UpdateUser(
            Guid id,
            UpdateUserDTO updateUserDTO
        )
        {
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var user = await _userRepository.DeleteUser(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok("User deleted successfully");
        }

        [HttpGet("GetUserByEmail")]
        public async Task<ActionResult<UserDetailsDTO>> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [HttpGet("GetUserByCPF")]
        public async Task<ActionResult<UserDetailsDTO>> GetUserByCPF([FromBody] string cpf)
        {
            var user = await _userRepository.GetUserByCPF(cpf);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<UserDetailsDTO>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            return Ok(users);
        }
    }
}
