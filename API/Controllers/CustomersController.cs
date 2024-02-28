using API.BL;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerBL _customerBL;
        public CustomersController(ICustomerBL customerBL)
        {
            _customerBL = customerBL;
        }

        // GET: api/customers
        [Authorize(Roles = "Admin,Member")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _customerBL.GetCustomers();
            return Ok(customers);
        }

        // GET: api/customers/{id}
        [Authorize(Roles = "Admin,Member")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerBL.GetCustomer(id);

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

            var customer = await _customerBL.CreateCustomer(customerDTO, User);
           
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

            await _customerBL.UpdateCustomer(id, customer);
            
            return Ok();
        }

        // DELETE: api/customers/{id}
        [Authorize(Roles = "Member")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerBL.DeleteCustomer(id);

            return Ok();
        }
    }
}