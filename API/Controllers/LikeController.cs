using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Member")]
    public class LikeController : ControllerBase
    {
        private readonly StoreContext _context;

        public LikeController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/like
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Like>>> GetLikes()
        {
            return await _context.Likes.ToListAsync();
        }

        // GET: api/like/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Like>> GetLike(int id)
        {
            var like = await _context.Likes.FindAsync(id);

            if (like == null)
            {
                return NotFound();
            }

            return like;
        }

        // POST: api/like
        [HttpPost]
        public async Task<ActionResult<Like>> CreateLike(Like like)
        {
            // Check if the user has already liked the product
            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(l => l.UserId == like.UserId && l.ProductId == like.ProductId);

            if (existingLike != null)
            {
                return BadRequest("User has already liked this product.");
            }

            _context.Likes.Add(like);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLike", new { id = like.LikeId }, like);
        }

        // GET: api/like/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetLikedProducts(int userId)
        {
            var likedProductIds = await _context.Likes
                .Where(l => l.UserId == userId)
                .Select(l => l.ProductId)
                .ToListAsync();

            var likedProducts = await _context.Products
                .Where(p => likedProductIds.Contains(p.Id))
                .ToListAsync();

            return likedProducts;
        }

        // DELETE: api/like/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLike(int id)
        {
            var like = await _context.Likes.FindAsync(id);
            if (like == null)
            {
                return NotFound();
            }

            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LikeExists(int id)
        {
            return _context.Likes.Any(e => e.LikeId == id);
        }
    }
}
