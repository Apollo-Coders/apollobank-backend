﻿using ApolloBank.DTOs;
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


        [Authorize]
        [HttpGet("GetUsers")]
        public async Task<ActionResult<UserDetailsDTO>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("GetAccount/{id}")]

        public async Task <ActionResult<Account>> GetAccountInformation(int id)
        {
            var account = await _accountRepository.GetAccountByAccountNumber(id); 
            return Ok(account);

        }


    }
}
