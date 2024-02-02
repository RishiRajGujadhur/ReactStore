using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly UserManager<User> _userManager;

        public ReceiptController(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Receipts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receipt>>> GetReceipts(int pageSize, int pageNumber)
        {
            var receipts = await _context.Receipts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return receipts;
        }

        [HttpGet("getMyReceipts")]
        public async Task<ActionResult<IEnumerable<Receipt>>> GetMyReceipts(int pageSize, int pageNumber)
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var receipts = await _context.Receipts
                .Where(r => r.UserId == user.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return receipts;
        }

        // GET: api/Receipts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Receipt>> GetReceipt(int id)
        {
            var Receipt = await _context.Receipts.FindAsync(id);

            if (Receipt == null)
            {
                return NotFound();
            }

            return Receipt;
        }

        // POST: api/Receipts 
        [HttpPost]
        public async Task<ActionResult<Receipt>> CreateReceipt(Receipt Receipt, string clientEmail)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            // Todo: Get details from future settings table
            Receipt.Sender = new ReceiptSender()
            {
                Address = "1234 Main St",
                City = "New York",
                Company = "Company Name",
                Zip = "123456",
                Country = "USA",
            };
            Receipt.BottomNotice = "Thank you for your business. Please make sure all payments are made within 2 weeks.";
            Receipt.DueDate = DateTime.UtcNow.AddDays(14);
            Receipt.IssueDate = DateTime.UtcNow;
            Receipt.Number = "INV-000" + Receipt.IssueDate.Date.ToString("yyyy-MM-dd") + "-" + Receipt.Id;
            Receipt.Logo = "https://via.placeholder.com/150";

            // create Receipt on order completion or when admin clicks on "payment is made" on order dashboard
            // create order 
            // create order items with Receipt id
            // get Receipt with order items
            // TODO: get from order

            // TODO: get from settings
            // Receipt.Settings = new ReceiptSettings(){
            //     Currency = "MUR",
            //     Format = "A4",
            //     Height = "210mm",
            //     Width = "297mm",
            //     Locale = "en-US",
            //     MarginBottom = 10,
            //     MarginLeft = 10,
            //     MarginRight = 10,
            //     MarginTop = 10,
            //     TaxNotation = "vat", 
            // };
            User client = GetUserByEmail(clientEmail);
            Receipt.UserId = client.Id;
            Receipt.Customer = client.Customer;

            _context.Receipts.Add(Receipt);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReceipt), new { id = Receipt.Id }, Receipt);
        }

        // PUT: api/Receipts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReceipt(int id, Receipt Receipt)
        {
            if (id != Receipt.Id)
            {
                return BadRequest();
            }

            _context.Entry(Receipt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptExists(id))
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

        // DELETE: api/Receipts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceipt(int id)
        {
            var Receipt = await _context.Receipts.FindAsync(id);
            if (Receipt == null)
            {
                return NotFound();
            }

            _context.Receipts.Remove(Receipt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #region Helper Methods
        private bool ReceiptExists(int id)
        {
            return _context.Receipts.Any(e => e.Id == id);
        }

        private User GetUserByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }
        #endregion
    }
}