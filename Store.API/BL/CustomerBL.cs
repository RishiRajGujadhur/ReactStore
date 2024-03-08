using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data;
using Store.Infrastructure.Data.DTOs;
using Store.Infrastructure.Entities;
using System.Security.Claims;

namespace Store.API.BL
{
    public interface ICustomerBL
    {
        UserManager<User> UserManager { get; }
        IMapper Mapper { get; }
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int id);
        Task<Customer> CreateCustomer(CreateCustomerDto customerDTO, ClaimsPrincipal User);
        Task UpdateCustomer(int id, Customer customer, ClaimsPrincipal User);
        Task DeleteCustomer(int id);
    }

    public class CustomerBL : ICustomerBL
    {
        public IMapper _mapper { get; }

        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public UserManager<User> UserManager => throw new NotImplementedException();

        public IMapper Mapper => throw new NotImplementedException();

        public CustomerBL(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            return customer;
        }

        public async Task<Customer> CreateCustomer(CreateCustomerDto customerDTO, ClaimsPrincipal User)
        {
            // Check if a customer already exists for the current user
            var existingCustomer = await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.User.UserName == User.Identity.Name);

            if (existingCustomer != null)
            {
                // If a customer already exists, return a conflict response
                throw new Exception("Customer already exists");
            }

            // If no customer exists, proceed with creating a new one
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var customer = new Customer
            {
                CreatedAtTimestamp = DateTime.UtcNow,
                LastModifiedUserName = User.Identity.Name,
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                Address = customerDTO.Address,
                Phone = customerDTO.Phone,
                User = user
            };

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task UpdateCustomer(int id, Customer customer, ClaimsPrincipal User)
        {
            customer.LastModifiedTimestamp = DateTime.UtcNow;
            customer.LastModifiedUserName = User.Identity.Name;
            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    throw new Exception("Customer not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id) ?? throw new Exception("Customer not found");
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        #region Private helper methods
        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
        #endregion
    }
}