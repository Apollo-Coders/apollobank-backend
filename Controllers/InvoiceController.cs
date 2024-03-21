using ApolloBank.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
       private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet("{id}")]
        public Task<IActionResult> Get
        {
           
        }
    }
}
