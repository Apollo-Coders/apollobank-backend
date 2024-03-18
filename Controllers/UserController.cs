﻿using ApolloBank.DTOs;
using ApolloBank.Repositories.Interfaces;
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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsDTO>> GetUser(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<UserDetailsDTO>> UpdateUser(Guid id, UpdateUserDTO updateUserDTO)
        {
            if (id != Guid.Parse(updateUserDTO.Id.ToString()))
            {
                return BadRequest("User not found.");
            }

            try
            {
                var updatedUser = await _userRepository.UpdateUser(updateUserDTO);
                if (updatedUser == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDetailsDTO>> DeleteUser(Guid id)
        {
            var user = await _userRepository.DeleteUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("GetUserByEmail")]
        public async Task<ActionResult<UserDetailsDTO>> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("GetUserByCPF")]
        public async Task<ActionResult<UserDetailsDTO>> GetUserByCPF(string cpf)
        {
            var user = await _userRepository.GetUserByCPF(cpf);
            if (user == null)
            {
                return NotFound();
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
