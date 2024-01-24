using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }
        public IMapper _mapper { get; }

        public CustomersController(StoreContext context, UserManager<User> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
        }

        // GET: api/customers
        [Authorize(Roles = "Admin,Member")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/customers/{id}
        [Authorize(Roles = "Admin,Member")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // POST: api/customers
        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] CreateCustomerDto customerDTO)
        {
            // Validate the DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if a customer already exists for the current user
            var existingCustomer = await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.User.UserName == User.Identity.Name);

            if (existingCustomer != null)
            {
                // If a customer already exists, return a conflict response
                return Conflict("Customer already exists for the current user.");
            }

            // If no customer exists, proceed with creating a new one
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var customer = _mapper.Map<Customer>(customerDTO);
            customer.User = user;

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerID }, customer);
        }

        // PUT: api/customers/{id}
        [Authorize(Roles = "Member")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // DELETE: api/customers/{id}
        [Authorize(Roles = "Member")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }

    }
}