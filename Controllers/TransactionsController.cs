using ApolloBank.DTOs;
using ApolloBank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionsController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TransactionDTO transactionDTO)
        {
            if (transactionDTO == null)
                return BadRequest("Data Invalid");

            await _transactionService.AddTransaction(transactionDTO);

             return Ok(); 
        }
    }
}
