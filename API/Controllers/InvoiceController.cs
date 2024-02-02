using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly StoreContext _context; 
        private readonly UserManager<User> _userManager;

        public InvoiceController(StoreContext context,UserManager<User> userManager)
        {
            _context = context; 
            _userManager = userManager;
        }

        // GET: api/invoices
        [HttpGet]
        [Authorize(Roles = "Admin")]  
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices(int pageSize, int pageNumber)
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var Invoices = await _context.Invoices
                .Where(r => r.UserId == user.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Invoices;
        }

        // GET: api/invoices/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        [HttpGet("getMyInvoices")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetMyInvoices(int pageSize, int pageNumber)
        {

           var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var Invoices = await _context.Invoices
                .Where(r => r.UserId == user.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Invoices;
        }

        // POST: api/invoices
        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateInvoice(Invoice invoice, string clientEmail)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name); 
            // Todo: Get details from future settings table
            invoice.Sender = new InvoiceSender(){
                Address = "1234 Main St",
                City = "New York",
                Company = "Company Name",
                Zip = "123456",
                Country = "USA", 
            };
            invoice.BottomNotice = "Thank you for your business. Please make sure all payments are made within 2 weeks.";
            invoice.DueDate = DateTime.UtcNow.AddDays(14);
            invoice.IssueDate = DateTime.UtcNow;
            invoice.Number = "INV-000" + invoice.IssueDate.Date.ToString("yyyy-MM-dd") + "-" + invoice.Id; 
            invoice.Logo = "https://via.placeholder.com/150";

            // create invoice on order completion
            // create order 
            // create order items with invoice id
            // get invoice with order items
            // TODO: get  from order

            // TODO: get from settings
            invoice.Settings = new InvoiceSettings(){
                Currency = "MUR",
                Format = "A4",
                Height = "210mm",
                Width = "297mm",
                Locale = "en-US",
                MarginBottom = 10,
                MarginLeft = 10,
                MarginRight = 10,
                MarginTop = 10,
                TaxNotation = "vat", 
            };
            User client = GetUserByEmail(clientEmail);
            invoice.UserId = client.Id; 

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
        }
        
        // PUT: api/invoices/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateInvoice(int id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/invoices/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #region Helper Methods
        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }

        private User GetUserByEmail(string email){
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }
        #endregion
    }
}