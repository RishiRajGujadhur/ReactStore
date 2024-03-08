using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data;
using Store.Infrastructure.Entities;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogisticsController : ControllerBase
    {
        private readonly StoreContext _context;

        public LogisticsController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/logistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Logistics>>> GetLogistics()
        {
            return await _context.Logistics.ToListAsync();
        }

        // GET: api/logistics/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Logistics>> GetLogisticsStatus(int id)
        {
            var logisticsStatus = await _context.Logistics.FindAsync(id);

            if (logisticsStatus == null)
            {
                return NotFound();
            }

            return logisticsStatus;
        }

        // POST: api/logistics
        [HttpPost]
        public async Task<ActionResult<Logistics>> CreateLogisticsStatus(Logistics logisticsStatus)
        {
            _context.Logistics.Add(logisticsStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLogisticsStatus), new { id = logisticsStatus.LogisticsID }, logisticsStatus);
        }

        // PUT: api/logistics/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLogisticsStatus(int id, Logistics logisticsStatus)
        {
            if (id != logisticsStatus.LogisticsID)
            {
                return BadRequest();
            }

            _context.Entry(logisticsStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogisticsStatusExists(id))
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

        // DELETE: api/logistics/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogisticsStatus(int id)
        {
            var logisticsStatus = await _context.Logistics.FindAsync(id);
            if (logisticsStatus == null)
            {
                return NotFound();
            }

            _context.Logistics.Remove(logisticsStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogisticsStatusExists(int id)
        {
            return _context.Logistics.Any(e => e.LogisticsID == id);
        }
    }
}