using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    [Route("Transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

      
        
        [HttpPost("AddTransaction")]
        public async Task<ActionResult> AddTransaction([FromBody] TransactionDTO transactionDTO)
        {
           try
            {
                if (transactionDTO == null)
                    return BadRequest("Data Invalid");

                var transactionResul = await _transactionService.AddTransaction(transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.Account_Id }, transactionResul);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("MakeWithdrawal")]
        public async Task<ActionResult> MakeWithdrawal([FromBody] TransactionDTO transactionDTO)
        {
            try
            {
                if (transactionDTO == null)
                    return BadRequest("Dados inválidos");

                var transactionResul = await _transactionService.Makewithdrawal(transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.Account_Id }, transactionResul);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("Makedeposit")]
        public async Task<ActionResult> Makedeposit([FromBody] TransactionDTO transactionDTO)
        {
            try 
            {
                if (transactionDTO == null)
                    return BadRequest("Data Invalid");

                var transactionResul = await _transactionService.Makedeposit(transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.Account_Id }, transactionResul);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Scheduletransaction")]
        public async Task<ActionResult> Scheduletransaction([FromBody] TransactionDTO transactionDTO)
        {
            try
            {
                if (transactionDTO == null)
                    return BadRequest("Data Invalid");

                var transactionResul = await _transactionService.Scheduletransaction(transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.Account_Id }, transactionResul);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetCurrentMonthTransactions/{id}")]
        public async Task<ActionResult<TransactionDTO>> GetCurrentMonthTransactions(int id)
        {
            var Transactions = await _transactionService.GetCurrentMonthTransactions(id);
            if (Transactions == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(Transactions);
        }

        [HttpGet("GetLastSixMonthsTransactions/{id}")]
        public async Task<ActionResult<TransactionDTO>> GetLastSixMonthsTransactions(int id)
        {
            var Transactions = await _transactionService.GetLastSixMonthsTransactions(id);
            if (Transactions == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(Transactions);
        }

        [HttpGet("GetAllTransactions/{id}")]
        public async Task<ActionResult<TransactionDTO>> GetAllTransactions(int id)
        {
            var Transactions = await _transactionService.GetAllTransactions(id);
            if (Transactions == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(Transactions);
        }


        [HttpGet("GetTransaction/{transaction_id}/{account_id}")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(int transaction_id, int account_id)
        {
            var Transactions = await _transactionService.GetTransaction(transaction_id, account_id);
            if (Transactions == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(Transactions);
        }


     

       
    }
}