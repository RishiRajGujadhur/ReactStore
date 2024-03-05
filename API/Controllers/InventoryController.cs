using API.Data;
using API.Entities;
using API.Integrations.Services.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IConfiguration _configuration;
        private readonly ProducerService _producerService;
        public InventoryController(StoreContext context, IConfiguration configuration, ProducerService producerService)
        {
            _context = context;
            _configuration = configuration;
            _producerService = producerService;
        }

        // GET: api/inventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventory()
        {
            return await _context.Inventories.ToListAsync();
        }

        // GET: api/inventory/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventoryItem(int id)
        {
            var inventoryItem = await _context.Inventories.FindAsync(id);

            if (inventoryItem == null)
            {
                return NotFound();
            }

            return inventoryItem;
        }
   
        // PUT: api/inventory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryItem(int id, Inventory inventoryItem)
        {
            if (id != inventoryItem.InventoryID)
            {
                return BadRequest();
            }

            _context.Entry(inventoryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryItemExists(id))
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

        // DELETE: api/inventory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            var inventoryItem = await _context.Inventories.FindAsync(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            _context.Inventories.Remove(inventoryItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryItemExists(int id)
        {
            return _context.Inventories.Any(e => e.InventoryID == id);
        }
    }
}