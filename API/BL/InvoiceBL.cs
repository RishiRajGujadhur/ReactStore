using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.DTOs.Invoice;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface IInvoiceBL
    {
        Task<IEnumerable<InvoiceDto>> GetAllInvoiceList(int pageSize, int pageNumber);
        Task<InvoiceDetailsDto> GetInvoice(int id);
        Task<IEnumerable<InvoiceDto>> GetMyInvoiceList(int pageSize, int pageNumber);
        Task<InvoiceSender> CreateOrUpdateInvoiceSender(InvoiceSender invoiceSender);
        Task<InvoiceSender> GetInvoiceSender();
        Task<Invoice> CreateInvoice(Invoice invoice, string clientEmail);
        Task SaveInvoiceSettings(InvoiceSettingsDto invoiceSettingsDto);
        Task UpdateInvoiceSettings(InvoiceSettingsDto invoiceSettingsDto);
        Task<InvoiceSettings> GetFirstInvoiceSettings();
        Task UpdateInvoice(int id, Invoice invoice);
        Task DeleteInvoice(int id);
    }

    public class InvoiceBL : IInvoiceBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public InvoiceBL(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task<IEnumerable<InvoiceDto>> GetAllInvoiceList(int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceDetailsDto> GetInvoice(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InvoiceDto>> GetMyInvoiceList(int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceSender> CreateOrUpdateInvoiceSender(InvoiceSender invoiceSender)
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceSender> GetInvoiceSender()
        {
            throw new NotImplementedException();
        }

        public Task<Invoice> CreateInvoice(Invoice invoice, string clientEmail)
        {
            throw new NotImplementedException();
        }

        public Task SaveInvoiceSettings(InvoiceSettingsDto invoiceSettingsDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInvoiceSettings(InvoiceSettingsDto invoiceSettingsDto)
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceSettings> GetFirstInvoiceSettings()
        {
            throw new NotImplementedException();
        }

        public Task UpdateInvoice(int id, Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public Task DeleteInvoice(int id)
        {
            throw new NotImplementedException();
        }
    }
}