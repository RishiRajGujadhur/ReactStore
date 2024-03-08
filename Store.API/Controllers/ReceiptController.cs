using API.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data;
using Store.Infrastructure.Data.DTOs.Receipt;
using Store.Infrastructure.Entities;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ReceiptController(StoreContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;

            _mapper = mapper;
        }

        // GET: api/Receipts
        [HttpGet("getAllReceiptList")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ReceiptDto>>> GetAllReceiptList(int pageSize, int pageNumber)
        {
            var receipts = await _context.Receipts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var receiptsDto = _mapper.Map<List<ReceiptDto>>(receipts);

            return Ok(receiptsDto);
        }

        [HttpGet("getMyReceiptList")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<IEnumerable<ReceiptDto>>> GetMyReceiptList(int pageSize, int pageNumber)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var receipts = await _context.Receipts
                .Where(r => r.UserId == user.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var receiptsDto = _mapper.Map<List<ReceiptDto>>(receipts);

            return Ok(receiptsDto);
        }

        // GET: api/Receipts/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<Receipt>> GetMyReceipt(int id)
        {
            int userId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            var receipt = await _context.Receipts
                .Include(u => u.User.Address)
                .Include(s => s.Sender)
                .Include(s => s.Settings)
                .Include(o => o.OrderItems)
                .Where(r => r.UserId == userId && r.Id == id)
                .FirstOrDefaultAsync();

            if (receipt == null)
            {
                return NotFound();
            }

            return receipt;
        }

        // POST: api/Receipts 
        [HttpPost]
        [Authorize(Roles = "Admin")]
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

            _context.Receipts.Add(Receipt);
            await _context.SaveChangesAsync();

            return NoContent();
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