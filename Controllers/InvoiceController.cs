using ApolloBank.DTOs;
using ApolloBank.Services;
using ApolloBank.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService) 
        {
            _invoiceService = invoiceService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetActualMonthInvoice()
        {
            
        }

        [HttpGet()]
        public async Task<IEnumerable<IActionResult>> GetAllInvoices()
        {

        }

        [HttpPost("/{cardNum}")]
        public async Task<IActionResult> PayParcialMonthInvoice(string cardNum)
        {
            DateTime datetime = DateTime.Now;

            try
            {
                await _invoiceService.PayParcialMonthInvoice(cardNum, datetime);
                return Ok();
            } catch(Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> PayTotalMonthInvoice()
        {

        }
    }
}
