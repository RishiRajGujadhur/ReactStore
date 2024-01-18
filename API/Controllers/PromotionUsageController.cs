using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionUsageController : ControllerBase
    {
        private readonly StoreContext _context;

        public PromotionUsageController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/promotionusage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromotionUsage>>> GetPromotionUsages()
        {
            return await _context.PromotionUsages.ToListAsync();
        }

        // GET: api/promotionusage/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PromotionUsage>> GetPromotionUsage(int id)
        {
            var promotionUsage = await _context.PromotionUsages.FindAsync(id);

            if (promotionUsage == null)
            {
                return NotFound();
            }

            return promotionUsage;
        }

        // POST: api/promotionusage
        [HttpPost]
        public async Task<ActionResult<PromotionUsage>> CreatePromotionUsage(PromotionUsage promotionUsage)
        {
            _context.PromotionUsages.Add(promotionUsage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPromotionUsage), new { id = promotionUsage.UsageID }, promotionUsage);
        }

        // PUT: api/promotionusage/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePromotionUsage(int id, PromotionUsage promotionUsage)
        {
            if (id != promotionUsage.UsageID)
            {
                return BadRequest();
            }

            _context.Entry(promotionUsage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromotionUsageExists(id))
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

        // DELETE: api/promotionusage/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotionUsage(int id)
        {
            var promotionUsage = await _context.PromotionUsages.FindAsync(id);
            if (promotionUsage == null)
            {
                return NotFound();
            }

            _context.PromotionUsages.Remove(promotionUsage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PromotionUsageExists(int id)
        {
            return _context.PromotionUsages.Any(e => e.UsageID == id);
        }
    }
}