using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data;
using Store.Infrastructure.Entities;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReturnRequestController : ControllerBase
    {
        private readonly StoreContext _context;

        public ReturnRequestController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/returnrequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnRequest>>> GetReturnRequests()
        {
            return await _context.ReturnRequests.ToListAsync();
        }

        // GET: api/returnrequests/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnRequest>> GetReturnRequest(int id)
        {
            var returnRequest = await _context.ReturnRequests.FindAsync(id);

            if (returnRequest == null)
            {
                return NotFound();
            }

            return returnRequest;
        }

        // POST: api/returnrequests
        [HttpPost]
        public async Task<ActionResult<ReturnRequest>> CreateReturnRequest(ReturnRequest returnRequest)
        {
            _context.ReturnRequests.Add(returnRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReturnRequest), new { id = returnRequest.ReturnRequestID }, returnRequest);
        }

        // PUT: api/returnrequests/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReturnRequest(int id, ReturnRequest returnRequest)
        {
            if (id != returnRequest.ReturnRequestID)
            {
                return BadRequest();
            }

            _context.Entry(returnRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReturnRequestExists(id))
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

        // DELETE: api/returnrequests/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReturnRequest(int id)
        {
            var returnRequest = await _context.ReturnRequests.FindAsync(id);
            if (returnRequest == null)
            {
                return NotFound();
            }

            _context.ReturnRequests.Remove(returnRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReturnRequestExists(int id)
        {
            return _context.ReturnRequests.Any(e => e.ReturnRequestID == id);
        }
    }
}