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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardsService _creditCardsService;
        private readonly IAuthService _authService;

        public CreditCardController(ICreditCardsService creditCardsService, IAuthService authService)
        {
            _creditCardsService = creditCardsService;
            _authService = authService;
        }


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
