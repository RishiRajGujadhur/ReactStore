 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data;
using Store.Infrastructure.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingMethodController : ControllerBase
    {
        private readonly StoreContext _context;

        public ShippingMethodController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/shippingmethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingMethod>>> GetShippingMethods()
        {
            return await _context.ShippingMethods.ToListAsync();
        }

        // GET: api/shippingmethods/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingMethod>> GetShippingMethod(int id)
        {
            var shippingMethod = await _context.ShippingMethods.FindAsync(id);

            if (shippingMethod == null)
            {
                return NotFound();
            }

            return shippingMethod;
        }

        // POST: api/shippingmethods
        [HttpPost]
        public async Task<ActionResult<ShippingMethod>> CreateShippingMethod(ShippingMethod shippingMethod)
        {
            _context.ShippingMethods.Add(shippingMethod);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShippingMethod), new { id = shippingMethod.ShippingMethodID }, shippingMethod);
        }

        // PUT: api/shippingmethods/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShippingMethod(int id, ShippingMethod shippingMethod)
        {
            if (id != shippingMethod.ShippingMethodID)
            {
                return BadRequest();
            }

            _context.Entry(shippingMethod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingMethodExists(id))
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

        // DELETE: api/shippingmethods/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingMethod(int id)
        {
            var shippingMethod = await _context.ShippingMethods.FindAsync(id);
            if (shippingMethod == null)
            {
                return NotFound();
            }

            _context.ShippingMethods.Remove(shippingMethod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShippingMethodExists(int id)
        {
            return _context.ShippingMethods.Any(e => e.ShippingMethodID == id);
        }
    }
}