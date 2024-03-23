using ApolloBank.DTOs;
using ApolloBank.Migrations;
using ApolloBank.Models;
using ApolloBank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardsService _creditCardsService;

        public CreditCardController(ICreditCardsService creditCardsService)
        {
            _creditCardsService = creditCardsService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateCreditCard(CreateCreditCardDTO createCreditCardDTO)
        {
            try
            {
                var creditCard = await _creditCardsService.CreateCreditCard(createCreditCardDTO);

                return CreatedAtAction(nameof(GetAllCardByCardNumber), new { cardNum = creditCard.Number }, creditCard);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


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


        [HttpGet("{accountid}")]
        public async Task<IActionResult> GetCreditCardsByAccountId(int accountId)
        {
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


        [HttpGet("{accountid}")]
        public async Task<IActionResult> GetAllCardByCardNumber(int accountId)
        {
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
