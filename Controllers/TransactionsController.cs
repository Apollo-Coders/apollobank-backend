using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        /// <summary>
        /// Inicializa uma nova instância da classe TransactionsController.
        /// </summary>
        /// <param name="transactionService">O serviço de transações a ser injetado.</param>
        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
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
                    return BadRequest("Data Invalid");

                var transactionResul = await _transactionService.AddTransaction(transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.Account_Id }, transactionResul);

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

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.Account_Id }, transactionResul);
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
                    return BadRequest("Data Invalid");

                var transactionResul = await _transactionService.Makedeposit(transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.Account_Id }, transactionResul);
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
                    return BadRequest("Data Invalid");

                var transactionResul = await _transactionService.Scheduletransaction(transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.Account_Id }, transactionResul);
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
                    return BadRequest("Data Invalid");

                var transactionResul = await _transactionService.AddTransactionCredit(transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { transaction_id = transactionResul.Id, account_id = transactionResul.Account_Id }, transactionResul);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Endpoint utilizado para obter as transações do mês atual para uma determinada conta.
        /// </summary>
        /// <param name="id">O ID da conta para a qual as transações devem ser obtidas.</param>
        /// <returns>ActionResult contendo as transações do mês atual.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetCurrentMonthTransactions(int id)
        {
            var Transactions = await _transactionService.GetCurrentMonthTransactions(id);
            if (Transactions == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(Transactions);
        }



        /// <summary>
        /// Endpoint utilizado para obter as transações dos últimos seis meses para uma determinada conta.
        /// </summary>
        /// <param name="id">O ID da conta para a qual as transações devem ser obtidas.</param>
        /// <returns>ActionResult contendo as transações dos últimos seis meses.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetLastSixMonthsTransactions(int id)
        {
            var Transactions = await _transactionService.GetLastSixMonthsTransactions(id);
            if (Transactions == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(Transactions);
        }



        /// <summary>
        /// Endpoint utilizado para obter todas as transações para uma determinada conta.
        /// </summary>
        /// <param name="id">O ID da conta para a qual as transações devem ser obtidas.</param>
        /// <returns>ActionResult contendo todas as transações da conta.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetAllTransactions(int id)
        {
            var Transactions = await _transactionService.GetAllTransactions(id);
            if (Transactions == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(Transactions);
        }



        /// <summary>
        /// Endpoint utilizado para obter uma transação específica com base no ID da transação e no ID da conta.
        /// </summary>
        /// <param name="transaction_id">O ID da transação.</param>
        /// <param name="account_id">O ID da conta associada à transação.</param>
        /// <returns>ActionResult contendo a transação específica.</returns>
        [HttpGet("{transaction_id}/{account_id}")]
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