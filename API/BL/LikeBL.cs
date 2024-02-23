using System.Security.Claims;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface ILikeBL
    {
        Task<IEnumerable<Like>> GetLikes();
        Task<Like> GetLike(int id);
        Task<bool> UserLikedProduct(int productId, ClaimsPrincipal User);
        Task UnlikeProduct(int productId, ClaimsPrincipal User);
        Task CreateLike(int productId, ClaimsPrincipal User);
        Task<IEnumerable<Product>> GetLikedProducts(ClaimsPrincipal User);
    }

    public class LikeBL : ILikeBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public LikeBL(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task CreateLike(int productId, ClaimsPrincipal User)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Check if the user has already liked the product
            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(l => l.UserId == user.Id && l.ProductId == productId);

            if (existingLike != null)
            {
                //return BadRequest("User has already liked this product.");
            }

            Like like = new()
            {
                UserId = user.Id,
                ProductId = productId
            };

            _context.Likes.Add(like);
            await _context.SaveChangesAsync(); 
        }

        public async Task<Like> GetLike(int id)
        {
            var like = await _context.Likes.FindAsync(id) ?? throw new KeyNotFoundException();
            return like;
        }

        public async Task<IEnumerable<Product>> GetLikedProducts(ClaimsPrincipal User)
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

        public async Task<IEnumerable<Like>> GetLikes()
        {
            return await _context.Likes.ToListAsync();
        }

        public async Task UnlikeProduct(int productId, ClaimsPrincipal User)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name); 
            // Check if the user has liked the specified product
            var like = await _context.Likes
                .FirstOrDefaultAsync(l => l.ProductId == productId && l.UserId == user.Id);

            if (like == null)
            {
                // If the like entry doesn't exist, return NotFound or BadRequest, depending on your preference.
                //return Throw($"User {user.Id} never liked product {productId}");
            }

            // Remove the like entry from the database
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> UserLikedProduct(int productId, ClaimsPrincipal User)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            // Check if the user has liked the specified product
            var like = await _context.Likes
                .FirstOrDefaultAsync(l => l.ProductId == productId && l.UserId == user.Id);
            return like != null;
        }
    }
}