using ApolloBank.DTOs;
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

        [HttpPost("add")]
        public async Task<ActionResult> AddTransaction([FromBody] TransactionDTO transactionDTO)
        {
            if (transactionDTO == null)
                return BadRequest("Data Invalid");

            await _transactionService.AddTransaction(transactionDTO);

            return Ok();
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult> MakeWithdrawal([FromBody] TransactionDTO transactionDTO)
        {
            if (transactionDTO == null)
                return BadRequest("Data Invalid");

            await _transactionService.Makewithdrawal(transactionDTO);

            return Ok();
        }

        [HttpPost("makedeposit")]
        public async Task<ActionResult> Makedeposit([FromBody] TransactionDTO transactionDTO)
        {
            if (transactionDTO == null)
                return BadRequest("Data Invalid");

            await _transactionService.Makedeposit(transactionDTO);

            return Ok();
        }

        [HttpGet("currentmonth/{id}")]
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
        
        [HttpGet]
        public async Task<ActionResult<TransactionDTO>> GetScheduledTransaction()
        {
            var produto = await _transactionService.GetScheduledTransaction();
            if (produto == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(produto);
        }


        [HttpPost("CompleteScheduled/{id}")]
        public async Task<ActionResult<bool>> CompleteScheduledTransaction(int? id)
        {
            var produto = await _transactionService.CompleteScheduledTransaction(id);
            if (produto == false)
            {
                return NotFound("Transaction not found");
            }
            return true;
        }


        [HttpPost("Scheduletransaction")]
        public async Task<ActionResult> Scheduletransaction([FromBody] TransactionDTO transactionDTO)
        {
            if (transactionDTO == null)
                return BadRequest("Data Invalid");

            await _transactionService.Scheduletransaction(transactionDTO);

            return Ok();
        }

       
    }
}