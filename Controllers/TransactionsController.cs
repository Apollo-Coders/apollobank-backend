using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Services;
using ApolloBank.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IAuthService _authService;

        public TransactionsController(ITransactionService transactionService, IAuthService authService)
        {
            _transactionService = transactionService;
            _authService = authService;
        }

      
        
        [HttpPost()]
        public async Task<ActionResult> AddTransaction([FromBody] TransactionDTO transactionDTO)
        {
           try
            {
                if (transactionDTO == null)
                    return BadRequest("Data Invalid");

                var transactionResul = await _transactionService.AddTransaction(transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.AccountId }, transactionResul);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<ActionResult> MakeWithdrawal([FromBody] TransactionDTO transactionDTO)
        {
            try
            {
                if (transactionDTO == null)
                    return BadRequest("Dados inválidos");

                var transactionResul = await _transactionService.Makewithdrawal(transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.AccountId }, transactionResul);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost()]
        public async Task<ActionResult> Makedeposit([FromBody] TransactionDTO transactionDTO)
        {
            try 
            {
                if (transactionDTO == null)
                    return BadRequest("Data Invalid");

                var transactionResul = await _transactionService.Makedeposit(transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.AccountId }, transactionResul);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<ActionResult> Scheduletransaction([FromBody] TransactionDTO transactionDTO)
        {
            try
            {
                if (transactionDTO == null)
                    return BadRequest("Data Invalid");

                var transactionResul = await _transactionService.Scheduletransaction(transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.AccountId }, transactionResul);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost()]
        public async Task<ActionResult> AddTransactionCredit([FromBody] TransactionDTO transactionDTO)
        {
            try
            {
                if (transactionDTO == null)
                    return BadRequest("Data Invalid");

                var transactionResul = await _transactionService.AddTransactionCredit(transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.AccountId }, transactionResul);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet()]
        public async Task<ActionResult<TransactionDTO>> GetCurrentMonthTransactions()
        {
            int accountId = _authService.GetTokenDateByHtppContext(HttpContext).AccountId;

            var Transactions = await _transactionService.GetCurrentMonthTransactions(accountId);
            if (Transactions == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(Transactions);
        }

        [HttpGet()]
        public async Task<ActionResult<TransactionDTO>> GetLastSixMonthsTransactions()
        {
            int accountId = _authService.GetTokenDateByHtppContext(HttpContext).AccountId;
            var Transactions = await _transactionService.GetLastSixMonthsTransactions(accountId);
            if (Transactions == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(Transactions);
        }

        [HttpGet()]
        public async Task<ActionResult<TransactionDTO>> GetAllTransactions()
        {
            int accountId = _authService.GetTokenDateByHtppContext(HttpContext).AccountId;
            var Transactions = await _transactionService.GetAllTransactions(accountId);
            if (Transactions == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(Transactions);
        }


        [HttpGet("{transactionId}")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(int transactionId)
        {
            int accountId = _authService.GetTokenDateByHtppContext(HttpContext).AccountId;
            var Transactions = await _transactionService.GetTransaction(transactionId, accountId);
            if (Transactions == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(Transactions);
        }


     

       
    }
}