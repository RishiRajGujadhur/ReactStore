using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        public UserManager<User> _userManager { get; }

        public LikeController(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        [HttpGet("user-liked")]
        public async Task<ActionResult<bool>> UserLikedProduct(int productId)
        {
             // Validate the DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Check if the user has liked the specified product
            var like = await _context.Likes
                .FirstOrDefaultAsync(l => l.ProductId == productId && l.UserId == user.Id);

            return Ok(like != null);
        }

        [HttpDelete("unlike")]
        public async Task<ActionResult> UnlikeProduct(int productId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Check if the user has liked the specified product
            var like = await _context.Likes
                .FirstOrDefaultAsync(l => l.ProductId == productId && l.UserId == user.Id);

            if (like == null)
            {
                // If the like entry doesn't exist, return NotFound or BadRequest, depending on your preference.
                return NotFound($"User {user.Id} never liked product {productId}");
            }

            // Remove the like entry from the database
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/like
        [HttpGet("like")]
        public async Task<ActionResult> CreateLike(int productId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name); 

            // Check if the user has already liked the product
            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(l => l.UserId == user.Id && l.ProductId == productId);

            if (existingLike != null)
            {
                return BadRequest("User has already liked this product.");
            }

            Like like = new()
            {
                UserId = user.Id,
                ProductId = productId
            };

            _context.Likes.Add(like);
            await _context.SaveChangesAsync();
           
            return NoContent();
        }

        // GET: api/like/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetLikedProducts()
        {
 
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var likedProductIds = await _context.Likes
                .Where(l => l.UserId == user.Id)
                .Select(l => l.ProductId)
                .ToListAsync();

            var likedProducts = await _context.Products
                .Where(p => likedProductIds.Contains(p.Id))
                .ToListAsync();

            return likedProducts;
        } 
    }
}
