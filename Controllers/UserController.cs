using System;
using System.Collections.Generic;
using System.Linq;
using ApolloBank.Data;
using ApolloBank.DTOs;
using ApolloBank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ApolloBankContext _appDbContext;

        public UserController(ApolloBankContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            var existingUser = _appDbContext.User.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return BadRequest("Usuário já cadastrado");
            }
            _appDbContext.User.Add(user);
            _appDbContext.SaveChanges();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var existingUser = _appDbContext.User.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }
            return existingUser;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            var existingUser = _appDbContext.User.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return BadRequest("Usuário não encontrado");
            }

            existingUser.FullName = updatedUser.FullName;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = updatedUser.Password;
            existingUser.DDD = updatedUser.DDD;
            existingUser.PhoneNumber = updatedUser.PhoneNumber;
            existingUser.BirthDay = updatedUser.BirthDay;
            existingUser.CPF = updatedUser.CPF;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var existingUser = _appDbContext.User.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return BadRequest("Usuário não encontrado");
            }
           _appDbContext.User.Remove(existingUser);
            return NoContent();
        }

        [HttpGet("GetUserByEmail")]
        public IActionResult GetUserByEmail(string email)
        {
            var existingUser = _appDbContext.User.FirstOrDefault(u => u.Email == email);
            if (existingUser == null)
            {
                return BadRequest("Usuário não encontrado");
            }
            return Ok(existingUser);
        }

        [HttpGet("GetUserByCPF")]
        public IActionResult GetUserByCPF(string cpf)
        {
            var existingUser = _appDbContext.User.FirstOrDefault(u => u.CPF == cpf);
            if (existingUser == null)
            {
                return BadRequest("Usuário não encontrado");
            }
            return Ok(existingUser);
        }
    }
}
