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

        /// <summary>
        /// Inicializa uma nova instância da classe TransactionsController.
        /// </summary>
        /// <param name="transactionService">O serviço de transações a ser injetado.</param>
        /// <param name="authService">Serviço de autenticação.</param>
        public TransactionsController(ITransactionService transactionService, IAuthService authService)
        {
            _transactionService = transactionService;
            _authService = authService;
        }


        /// <summary>
        /// Endpoint utilizado para adicionar uma nova transação.
        /// </summary>
        /// <param name="transactionDTO">Objeto contendo os dados da transação.</param>
        /// <returns>ActionResult que representa a resposta HTTP.</returns>
        [HttpPost()]
        public async Task<ActionResult> AddTransaction([FromBody] TransactionDTO transactionDTO)
        {
           try
            {
                if (transactionDTO == null)
                    return BadRequest("Dados inválidos");

                var transactionResul = await _transactionService.AddTransaction(transactionDTO);

                return Ok(201);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Endpoint utilizado para realizar um saque.
        /// </summary>
        /// <param name="transactionDTO">Objeto contendo os dados da transação de saque.</param>
        /// <returns>ActionResult que representa a resposta HTTP.</returns>
        [HttpPost()]
        public async Task<ActionResult> MakeWithdrawal([FromBody] TransactionDTO transactionDTO)
        {
            try
            {
                if (transactionDTO == null)
                    return BadRequest("Dados inválidos");

                var transactionResul = await _transactionService.Makewithdrawal(transactionDTO);

                return Ok(201);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Endpoint utilizado para realizar um depósito.
        /// </summary>
        /// <param name="transactionDTO">Objeto contendo os dados da transação de depósito.</param>
        /// <returns>ActionResult que representa a resposta HTTP.</returns>
        [HttpPost()]
        public async Task<ActionResult> Makedeposit([FromBody] TransactionDTO transactionDTO)
        {
            try 
            {
                if (transactionDTO == null)
                    return BadRequest("Dados inválidos");

                var transactionResul = await _transactionService.Makedeposit(transactionDTO);

                return Ok(201); // CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.AccountId }, transactionResul);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Endpoint utilizado para agendar uma transação.
        /// </summary>
        /// <param name="transactionDTO">Objeto contendo os dados da transação a ser agendada.</param>
        /// <returns>ActionResult que representa a resposta HTTP.</returns>
        [HttpPost()]
        public async Task<ActionResult> Scheduletransaction([FromBody] TransactionDTO transactionDTO)
        {
            try
            {
                if (transactionDTO == null)
                    return BadRequest("Dados inválidos");

                var transactionResul = await _transactionService.Scheduletransaction(transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.AccountId }, transactionResul);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Endpoint utilizado para adicionar uma transação de crédito.
        /// </summary>
        /// <param name="transactionDTO">Objeto contendo os dados da transação de crédito a ser adicionada.</param>
        /// <returns>ActionResult que representa a resposta HTTP.</returns>
        [HttpPost()]
        public async Task<ActionResult> AddTransactionCredit([FromBody] TransactionDTO transactionDTO)
        {
            try
            {
                if (transactionDTO == null)
                    return BadRequest("Dados inválidos");

                var transactionResul = await _transactionService.AddTransactionCredit(transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.AccountId }, transactionResul);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Endpoint utilizado para obter as transações do mês atual para uma determinada conta.
        /// </summary>
        /// <returns>ActionResult contendo as transações do mês atual.</returns>
        [HttpGet()]
        public async Task<ActionResult<TransactionDTO>> GetCurrentMonthTransactions()
        {
            int accountId = _authService.GetTokenDateByHtppContext(HttpContext).AccountId;

            var Transactions = await _transactionService.GetCurrentMonthTransactions(accountId);
            if (Transactions == null)
            {
                return NotFound("Transação não encontrada");
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
                return NotFound("Transação não encontrada");
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
                return NotFound("Transação não encontrada");
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
                return NotFound("Transação não encontrada");
            }
            return Ok(Transactions);
        }


     

       
    }
}