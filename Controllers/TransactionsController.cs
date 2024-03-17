using ApolloBank.DTOs;
using ApolloBank.Services;
using ApolloBank.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
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

        [HttpGet("current/{id}")]
        public async Task<ActionResult<TransactionDTO>> GetCurrentMonthTransactions(int id)
        {
            var produto = await _transactionService.GetCurrentMonthTransactions(id);
            if (produto == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(produto);
        }

        [HttpGet("lastsix/{id}")]
        public async Task<ActionResult<TransactionDTO>> GetLastSixMonthsTransactions(int id)
        {
            var produto = await _transactionService.GetLastSixMonthsTransactions(id);
            if (produto == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(produto);
        }

        [HttpGet("all/{id}")]
        public async Task<ActionResult<TransactionDTO>> GetAllTransactions(int id)
        {
            var produto = await _transactionService.GetAllTransactions(id);
            if (produto == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(produto);
        }

    }
}
