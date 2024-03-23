using ApolloBank.DTOs;
using ApolloBank.Migrations;
using ApolloBank.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly ICreditCardsRepository _creditCardsRepository;

        public CreditCardController(ICreditCardRepository creditCardRepository, ICreditCardsRepository creditCardsRepository)
        {
            _creditCardRepository = creditCardRepository;
            _creditCardsRepository = creditCardsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCreditCard(CreateCreditCardDTO createCreditCardDTO)
        {
            try
            {
                var creditCard = await _creditCardsRepository.CreateCreditCard(createCreditCardDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{number}")]
        public async Task<IActionResult> BlockCreditCard(string number)
        {
            try
            {
                var blockedCreditCard = await _creditCardsRepository.BlockCreditCard(number);

                if (blockedCreditCard == null)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
