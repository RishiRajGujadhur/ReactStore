using API.Data;
using API.DTOs;
using API.DTOs.Invoice;
using API.Entities;
using API.Extensions;
using API.RequestHelpers;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public InvoiceController(StoreContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: api/invoices
        [HttpGet("getAllInvoiceList")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetAllInvoiceList(int pageSize, int pageNumber)
        {
            var invoices = await _context.Invoices
                .OrderByDescending(i => i.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var invoicesDto = _mapper.Map<List<InvoiceDto>>(invoices);

            return invoicesDto;
        }

        // GET: api/invoices/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDetailsDto>> GetInvoice(int id)
        {
            var invoice = await _context.Invoices
                .Include(s => s.Sender)
                .Include(u => u.User)
                .Include(u => u.User.Address)
                .Include(s => s.Settings)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }
            var invoiceDetailsDto = _mapper.Map<InvoiceDetailsDto>(invoice);
            invoiceDetailsDto.Customer = new CustomerDto
            {
                Name = invoice.User.Address?.FullName,
                Address = invoice.User.Address?.Address1,
                City = invoice.User.Address?.City,
                Country = invoice.User.Address?.Country,
                Zip = invoice.User.Address?.Zip
            };

            invoiceDetailsDto.Products = invoice.OrderItems.Select(item => new ProductItemsDto
            {
                Description = item.ItemOrdered.Name,
                Price = item.Price,
                Quantity = item.Quantity,
                TaxRate = 15
            }).ToList();

            return invoiceDetailsDto;
        }

        [HttpGet("getMyInvoiceList")]
        public async Task<IEnumerable<InvoiceDto>> GetMyInvoiceList(int pageSize, int pageNumber)
        {
            // pageNumber = pageNumber == 0 ? 1 : pageNumber;  
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            var invoices = await _context.Invoices
                .Where(r => r.User.Id == user.Id)
                .OrderByDescending(i => i.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var invoicesDto = _mapper.Map<List<InvoiceDto>>(invoices);
            await AddPaginationMetadata(pageSize, pageNumber, user);
            return invoicesDto;
        }

        // POST: api/invoices
        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateInvoice(Invoice invoice, string clientEmail)
        {
            // Todo: Get details from future settings table
            invoice.Sender = new InvoiceSender()
            {
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

            invoice.Settings = _context.InvoiceSettings.FirstOrDefault();

            User client = GetUserByEmail(clientEmail);
            invoice.UserId = client.Id;

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return NoContent(); ;
        }

        // POST: api/invoices/saveInvoiceSettings
        [HttpPost("saveInvoiceSettings")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SaveInvoiceSettings(InvoiceSettingsDto invoiceSettingsDto)
        {
            var invoiceSettings = _mapper.Map<InvoiceSettings>(invoiceSettingsDto);
            _context.InvoiceSettings.Add(invoiceSettings);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/invoices/getFirstInvoiceSettings
        [HttpGet("getFirstInvoiceSettings")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<InvoiceSettings>> GetFirstInvoiceSettings()
        {
            var firstInvoiceSettings = await _context.InvoiceSettings.FirstOrDefaultAsync();
            if (firstInvoiceSettings == null)
            {
                return NotFound();
            }

            return firstInvoiceSettings;
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

        private User GetUserByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        private async Task AddPaginationMetadata(int pageSize, int pageNumber, User user)
        {
            var allMyInvoices = await _context.Invoices
                    .Where(r => r.User.Id == user.Id)
                    .Select(x => x.Id)
                    .ToListAsync();
            var totalNumberOfRows = allMyInvoices.Count;
            MetaData metaData = new MetaData()
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalNumberOfRows / (double)pageSize),
                TotalCount = totalNumberOfRows
            };
            Response.AddPaginationHeader(metaData);
        }

        #endregion
    }

}