using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data;
using Store.Infrastructure.Entities;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Member")]
    public class CollectionListController : ControllerBase
    {
        private readonly StoreContext _context;

        public CollectionListController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/CollectionList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionList>>> GetCollectionLists()
        {
            return await _context.CollectionLists.Include(w => w.CollectionListItems).ToListAsync();
        }

        // GET: api/CollectionList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CollectionList>> GetCollectionList(int id)
        {
            var CollectionList = await _context.CollectionLists.Include(w => w.CollectionListItems).FirstOrDefaultAsync(w => w.CollectionListID == id);

            if (CollectionList == null)
            {
                return NotFound();
            }

            return CollectionList;
        }

        // POST: api/CollectionList
        [HttpPost]
        public async Task<ActionResult<CollectionList>> CreateCollectionList(CollectionList CollectionList)
        {
            _context.CollectionLists.Add(CollectionList);

            // Assuming that the products to be Collectioned are passed in the CollectionListItems collection
            if (CollectionList.CollectionListItems != null && CollectionList.CollectionListItems.Any())
            {
                foreach (var CollectionListItem in CollectionList.CollectionListItems)
                {
                    CollectionListItem.CollectionListID = CollectionList.CollectionListID;
                    _context.CollectionListItems.Add(CollectionListItem);
                }
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollectionList", new { id = CollectionList.CollectionListID }, CollectionList);
        }

        // PUT: api/CollectionList/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCollectionList(int id, CollectionList CollectionList)
        {
            if (id != CollectionList.CollectionListID)
            {
                return BadRequest();
            }

            _context.Entry(CollectionList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectionListExists(id))
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

        // DELETE: api/CollectionList/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollectionList(int id)
        {
            var CollectionList = await _context.CollectionLists.FindAsync(id);
            if (CollectionList == null)
            {
                return NotFound();
            }

            _context.CollectionLists.Remove(CollectionList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CollectionListExists(int id)
        {
            return _context.CollectionLists.Any(e => e.CollectionListID == id);
        }
    }
}
