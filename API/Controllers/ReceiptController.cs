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

        public ReceiptController(StoreContext context,UserManager<User> userManager)
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

        [HttpGet]
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
        public async Task<ActionResult<Receipt>> CreateReceipt(Receipt Receipt)
        {
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

        private bool ReceiptExists(int id)
        {
            return _context.Receipts.Any(e => e.Id == id);
        }
    }
}