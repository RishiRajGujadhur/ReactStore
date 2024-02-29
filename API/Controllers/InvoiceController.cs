using API.BL;
using API.DTOs;
using API.DTOs.Invoice;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceBL _invoiceBL;
        public InvoiceController(IInvoiceBL invoiceBL)
        {
            _invoiceBL = invoiceBL;
        }

        // GET: api/invoices
        [HttpGet("getAllInvoiceList")]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<InvoiceDto>> GetAllInvoiceList(int pageSize, int pageNumber)
        {
            return await _invoiceBL.GetAllInvoiceList(pageSize, pageNumber);
        }

        // GET: api/invoices/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDetailsDto>> GetInvoice(int id)
        {
            return await _invoiceBL.GetInvoice(id);
        }

        [HttpGet("getMyInvoiceList")]
        public async Task<IEnumerable<InvoiceDto>> GetMyInvoiceList(int pageSize, int pageNumber)
        {
            return await _invoiceBL.GetMyInvoiceList(HttpContext.Response, User, pageSize, pageNumber);
        }

        [HttpPost("createOrUpdateInvoiceSender")]
        public async Task<ActionResult<InvoiceSender>> CreateOrUpdateInvoiceSender(InvoiceSender invoiceSender)
        {
            return await _invoiceBL.CreateOrUpdateInvoiceSender(User, invoiceSender);
        } 

        // GET: api/invoices/getInvoiceSender/{userId}
        [HttpGet("getInvoiceSender")]
        public async Task<ActionResult<InvoiceSender>> GetInvoiceSender()
        {
            return await _invoiceBL.GetInvoiceSender(User);
        }

        // POST: api/invoices
        // For future usage, allowing an admin to create an invoice for a client without an order 
        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateInvoice(Invoice invoice, string clientEmail)
        {
            return await _invoiceBL.CreateInvoice(User, invoice, clientEmail);
        }

        // POST: api/invoices/saveInvoiceSettings
        [HttpPost("saveInvoiceSettings")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SaveInvoiceSettings(InvoiceSettingsDto invoiceSettingsDto)
        {
            await _invoiceBL.SaveInvoiceSettings(invoiceSettingsDto, User);

            return NoContent();
        }

        // PUT: api/invoices/updateInvoiceSettings
        [HttpPut("updateInvoiceSettings")]
        public async Task<IActionResult> UpdateInvoiceSettings(InvoiceSettingsDto invoiceSettingsDto)
        {
            await _invoiceBL.UpdateInvoiceSettings(invoiceSettingsDto, User);
            return NoContent();
        }

        // GET: api/invoices/getFirstInvoiceSettings
        [HttpGet("getFirstInvoiceSettings")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<InvoiceSettings>> GetFirstInvoiceSettings()
        {
            return await _invoiceBL.GetFirstInvoiceSettings();
        }

        // PUT: api/invoices/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateInvoice(int id, Invoice invoice)
        {
            await _invoiceBL.UpdateInvoice(id, invoice);
            return NoContent();
        }

        // DELETE: api/invoices/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            await _invoiceBL.DeleteInvoice(id);

            return NoContent();
        }
    }
}