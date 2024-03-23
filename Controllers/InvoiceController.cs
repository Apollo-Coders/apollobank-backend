using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Services;
using ApolloBank.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ApolloBank.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetActualMonthInvoice(int accountId)
        {
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

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAllInvoices(int accountId)
        {
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

        [HttpPost()]
        public async Task<IActionResult> PayTotalMonthInvoice(int accountId)
        {
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
