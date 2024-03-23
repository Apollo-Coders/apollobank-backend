using ApolloBank.Services;
using ApolloBank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IAuthService _authService;

        public InvoiceController(IInvoiceService invoiceService, IAuthService authService)
        {
            _invoiceService = invoiceService;
            _authService = authService;
        }


        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetActualMonthInvoice()
        {
            int accountId = _authService.GetTokenDateByHtppContext(HttpContext).AccountId;

            try
            {
                var actualMonthInvoice = await _invoiceService.GetActualMonthInvoice(accountId);
                if (actualMonthInvoice == null)
                {
                    return NotFound();
                }
                return Ok(actualMonthInvoice);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetAllInvoices()
        {
            int accountId = _authService.GetTokenDateByHtppContext(HttpContext).AccountId;

            try
            {
                var allInvoices = await _invoiceService.GetAllInvoices(accountId);
                if (allInvoices == null)
                {
                    return NotFound();
                }
                return Ok(allInvoices);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpPost("{cardNum}")]
        public async Task<IActionResult> PayParcialMonthInvoice(string cardNum)
        {
            DateTime datetime = DateTime.Now;

            try
            {
                await _invoiceService.PayParcialMonthInvoice(cardNum, datetime);
                return Ok();
            } catch(Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> PayTotalMonthInvoice()
        {
            int accountId = _authService.GetTokenDateByHtppContext(HttpContext).AccountId;
            DateTime datetime = DateTime.Now;

            try
            {
                await _invoiceService.PayTotalMonthInvoice(accountId, datetime);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
