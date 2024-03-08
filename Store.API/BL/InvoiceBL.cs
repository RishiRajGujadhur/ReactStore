using System.Security.Claims;
using API.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.API.Extentions;
using Store.API.RequestHelpers;
using Store.Infrastructure.Data;
using Store.Infrastructure.Data.DTOs.Invoice;
using Store.Infrastructure.Entities;

namespace Store.API.BL
{
    public interface IInvoiceBL
    {
        Task<IEnumerable<InvoiceDto>> GetAllInvoiceList(int pageSize, int pageNumber);
        Task<InvoiceDetailsDto> GetInvoice(int id);
        Task<IEnumerable<InvoiceDto>> GetMyInvoiceList(HttpResponse Response, ClaimsPrincipal User, int pageSize, int pageNumber);
        Task<InvoiceSender> CreateOrUpdateInvoiceSender(ClaimsPrincipal User, InvoiceSender invoiceSender);
        Task<InvoiceSender> GetInvoiceSender(ClaimsPrincipal User);
        Task<Invoice> CreateInvoice(ClaimsPrincipal User, Invoice invoice, string clientEmail);
        Task SaveInvoiceSettings(InvoiceSettingsDto invoiceSettingsDto, ClaimsPrincipal User);
        Task UpdateInvoiceSettings(InvoiceSettingsDto invoiceSettingsDto, ClaimsPrincipal User);
        Task<InvoiceSettings> GetFirstInvoiceSettings();
        Task UpdateInvoice(int id, Invoice invoice);
        Task DeleteInvoice(int id);
    }

    public class InvoiceBL : IInvoiceBL
    {

        private readonly ILogger<InvoiceBL> _logger;
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        private readonly IMapper _mapper;

        public InvoiceBL(StoreContext context, UserManager<User> userManager, IMapper mapper, ILogger<InvoiceBL> logger)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllInvoiceList(int pageSize, int pageNumber)
        {
            var invoices = await _context.Invoices
                .OrderByDescending(i => i.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var invoicesDto = _mapper.Map<List<InvoiceDto>>(invoices);

            return invoicesDto;
        }

        public async Task<InvoiceDetailsDto> GetInvoice(int id)
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
                throw new Exception("Invoice not found");
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

        public async Task<IEnumerable<InvoiceDto>> GetMyInvoiceList(HttpResponse Response, ClaimsPrincipal User, int pageSize, int pageNumber)
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
            await AddPaginationMetadata(pageSize, pageNumber, user, Response);
            return invoicesDto;
        }

        public async Task<InvoiceSender> CreateOrUpdateInvoiceSender(ClaimsPrincipal User, InvoiceSender invoiceSender)
        {
            int userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var existingInvoiceSenderRecord = await _context.InvoiceSenders.OrderByDescending(i => i.Id).FirstOrDefaultAsync(i => i.UserId == userId);
            if (existingInvoiceSenderRecord != null)
            {
                await UpdateInvoiceSender(invoiceSender, User, userId);
            }
            else
            {
                invoiceSender.UserId = userId;
                invoiceSender.CreatedByUserId = userId;
                invoiceSender.CreatedByUserName = User.Identity.Name;
                invoiceSender.CreatedAtTimestamp = DateTime.UtcNow;
                _context.InvoiceSenders.Add(invoiceSender);
                await _context.SaveChangesAsync();
            }
            return invoiceSender;
        }

        public async Task<InvoiceSender> GetInvoiceSender(ClaimsPrincipal User)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var invoiceSender = await _context.InvoiceSenders
            .Where(i => i.UserId == userId)
            .OrderByDescending(i => i.Id)
            .FirstOrDefaultAsync();

            if (invoiceSender == null)
            {
                throw new Exception("Invoice Sender not found");
            }
            return invoiceSender;
        }

        public async Task<Invoice> CreateInvoice(ClaimsPrincipal User, Invoice invoice, string clientEmail)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            GeneralSettings generalSettings = await _context.GeneralSettings.FirstOrDefaultAsync();
            invoice.Sender = _context.InvoiceSenders.Where(i => i.UserId == user.Id).FirstOrDefault();
            var invoiceSettings = _context.InvoiceSettings.OrderBy(i => i.Id).FirstOrDefault();

            invoice.CreatedAtTimestamp = DateTime.UtcNow;
            invoice.CreatedByUserId = user.Id;
            invoice.CreatedByUserName = user.UserName;

            invoice.BottomNotice = invoiceSettings.BottomNotice;
            invoice.DueDate = DateTime.UtcNow.AddDays(14);
            invoice.IssueDate = DateTime.UtcNow;
            invoice.Number = "INV-000" + invoice.IssueDate.Date.ToString("yyyy-MM-dd") + "-" + invoice.Id;
            invoice.Logo = generalSettings?.LogoURL;
            invoice.Settings = invoiceSettings;

            User client = GetUserByEmail(clientEmail);
            invoice.UserId = client.Id;

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task SaveInvoiceSettings(InvoiceSettingsDto invoiceSettingsDto, ClaimsPrincipal User)
        {
            var invoiceSettings = _mapper.Map<InvoiceSettings>(invoiceSettingsDto);

            invoiceSettings.CreatedAtTimestamp = DateTime.UtcNow;
            invoiceSettings.CreatedByUserId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            invoiceSettings.CreatedByUserName = User.Identity.Name;

            _context.InvoiceSettings.Add(invoiceSettings);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateInvoiceSettings(InvoiceSettingsDto invoiceSettingsDto, ClaimsPrincipal User)
        {
            try
            {
                var invoiceSettings = await _context.InvoiceSettings.OrderBy(i => i.Id).FirstOrDefaultAsync() ?? throw new Exception("Invoice settings not found");
                _mapper.Map(invoiceSettingsDto, invoiceSettings);

                invoiceSettings.LastModifiedTimestamp = DateTime.UtcNow;
                invoiceSettings.LastModifiedUserId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                invoiceSettings.LastModifiedUserName = User.Identity.Name;

                _context.Entry(invoiceSettings).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, LoggerExtention.AddErrorDetails(ex, User.Identity.Name, "An error occurred while updating invoice settings."));
                // Return a 500 Internal Server Error status code
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task<InvoiceSettings> GetFirstInvoiceSettings()
        {
            var firstInvoiceSettings = await _context.InvoiceSettings.OrderBy(i => i.Id).FirstOrDefaultAsync();
            if (firstInvoiceSettings == null)
            {
                throw new Exception("Invoice settings not found");
            }

            return firstInvoiceSettings;
        }

        public async Task UpdateInvoice(int id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                throw new Exception("Invoice not found");
            }

            invoice.LastModifiedTimestamp = DateTime.UtcNow;
            invoice.LastModifiedUserId = invoice.UserId;
            invoice.LastModifiedUserName = invoice.User.UserName;

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    throw new Exception("Invoice not found");
                }
                else
                {
                    throw new Exception("An error occurred while updating invoice.");
                }
            }
        }

        public async Task DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                throw new Exception("Invoice not found");
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
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

        private async Task AddPaginationMetadata(int pageSize, int pageNumber, User user, HttpResponse response)
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
            response.AddPaginationHeader(metaData);
        }

        private async Task<InvoiceSender> UpdateInvoiceSender(InvoiceSender updatedInvoiceSender, ClaimsPrincipal User, int userId)
        {
            var invoiceSender = await _context.InvoiceSenders
             .OrderByDescending(i => i.Id)
            .FirstOrDefaultAsync(i => i.UserId == updatedInvoiceSender.UserId
            && i.Id == updatedInvoiceSender.Id) ?? throw new Exception("Invoice Sender not found");

            invoiceSender.LastModifiedUserId = userId;
            invoiceSender.LastModifiedUserName = User.Identity.Name;
            invoiceSender.LastModifiedTimestamp = DateTime.UtcNow;
            invoiceSender.City = updatedInvoiceSender.City;
            invoiceSender.Zip = updatedInvoiceSender.Zip;
            invoiceSender.Country = updatedInvoiceSender.Country;
            invoiceSender.Address = updatedInvoiceSender.Address;
            invoiceSender.Company = updatedInvoiceSender.Company;

            _context.Entry(invoiceSender).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return invoiceSender;
        }
        #endregion
    }
}