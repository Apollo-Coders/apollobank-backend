using ApolloBank.Services;
using ApolloBank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    /// <summary>
    /// Controlador responsável por operações relacionadas a faturas (invoices).
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IAuthService _authService;

        /// <summary>
        /// Construtor da classe InvoiceController.
        /// </summary>
        /// <param name="invoiceService">Serviço de faturas.</param>
        /// <param name="authService">Serviço de autenticação.</param>
        public InvoiceController(IInvoiceService invoiceService, IAuthService authService)
        {
            _invoiceService = invoiceService;
            _authService = authService;
        }

        /// <summary>
        /// Endpoint utilizado para obter as faturas do mês atual para o usuário autenticado.
        /// </summary>
        /// <returns>Retorna as faturas do mês atual.</returns>
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

        /// <summary>
        /// Endpoint utilizado para obter todas as faturas para o usuário autenticado.
        /// </summary>
        /// <returns>Retorna todas as faturas do usuário.</returns>
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

        /// <summary>
        /// Endpoint utilizado para pagar parcialmente a fatura do mês atual com o número do cartão especificado.
        /// </summary>
        /// <param name="cardNum">Número do cartão.</param>
        /// <returns>Retorna Ok se a operação for bem-sucedida.</returns>
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

        /// <summary>
        /// Endpoint utilizado para pagar totalmente a fatura do mês atual para o usuário autenticado.
        /// </summary>
        /// <returns>Retorna Ok se a operação for bem-sucedida.</returns>
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
