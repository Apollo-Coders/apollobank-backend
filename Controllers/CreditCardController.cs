using ApolloBank.DTOs;
using ApolloBank.Migrations;
using ApolloBank.Models;
using ApolloBank.Services;
using ApolloBank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    /// <summary>
    /// Controller responsável pelas operações relacionadas a cartões de crédito.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardsService _creditCardsService;
        private readonly IAuthService _authService;
        /// <summary>
        /// Construtor do controlador de cartões de crédito.
        /// </summary>
        /// <param name="creditCardsService">Serviço para manipulação de cartões de crédito.</param>
        /// <param name="authService">Serviço para autenticação.</param>
        public CreditCardController(ICreditCardsService creditCardsService, IAuthService authService)
        {
            _creditCardsService = creditCardsService;
            _authService = authService;
        }

        /// <summary>
        /// Endpoint utilizado para a criação de um novo cartão de crédito.
        /// </summary>
        /// <remarks>
        /// Este endpoint cria um novo cartão de crédito para o usuário autenticado.
        /// </remarks>
        /// <returns>Retorna o cartão de crédito criado.</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCreditCard()
        {
            try
            {
                int accountId = _authService.GetTokenDateByHtppContext(HttpContext).AccountId;
                var creditCard = await _creditCardsService.CreateCreditCard(accountId);

                return CreatedAtAction(nameof(GetAllCardByCardNumber), new { cardNum = creditCard.Number }, creditCard);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint utilizado para bloquear um cartão de crédito.
        /// </summary>
        /// <param name="cardNum">Número do cartão a ser bloqueado.</param>
        /// <returns>Retorna uma resposta HTTP indicando o sucesso da operação.</returns>
        [Authorize]
        [HttpPut("{cardNum}")]
        public async Task<IActionResult> BlockCreditCard(string cardNum)
        {
            try
            {
                var blockedCreditCard = await _creditCardsService.BlockCreditCard(cardNum);

                if (blockedCreditCard == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint utilizado para obter os cartões de crédito associados à conta do usuário autenticado.
        /// </summary>
        /// <returns>Retorna os cartões de crédito do usuário.</returns>
        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetCreditCardsByAccountId()
        {
            int accountId = _authService.GetTokenDateByHtppContext(HttpContext).AccountId;

            try
            {
                var creditCard = await _creditCardsService.GetCreditCardsByAccountId(accountId);
                if (creditCard == null)
                {
                    return NotFound();
                }
                    return Ok(creditCard);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint utilizado para obter todos os cartões de crédito associados à conta do usuário autenticado.
        /// </summary>
        /// <returns>Retorna todos os cartões de crédito do usuário.</returns>
        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetAllCardByCardNumber()
        {
            int accountId = _authService.GetTokenDateByHtppContext(HttpContext).AccountId;

            try
            {
                var creditCard = await _creditCardsService.GetAllCardByAccountId(accountId);

                if (creditCard == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(creditCard);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Endpoint utilizado para definir o limite de um cartão de crédito.
        /// </summary>
        /// <param name="setLimitData">Dados para definir o limite do cartão.</param>
        /// <returns>Retorna uma resposta HTTP indicando o sucesso da operação.</returns>
        [Authorize]
        [HttpPut()]
        public async Task<IActionResult> SetCardLimit([FromBody] SetCardLimitDTO setLimitData)
        {
            double newLimit = setLimitData.NewLimit;
            int accountId = setLimitData.AccountId;
            string cardNum = setLimitData.CardNum;
            try
            {
                await _creditCardsService.SetCardLimit(newLimit, accountId, cardNum);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
