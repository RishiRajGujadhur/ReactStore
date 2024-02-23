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
        Task<bool> UserLikedProduct(int productId);
        Task UnlikeProduct(int productId);
        Task CreateLike(int productId);
        Task<IEnumerable<Product>> GetLikedProducts();
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

        public Task CreateLike(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<Like> GetLike(int id)
        {
           var like = await _context.Likes.FindAsync(id) ?? throw new KeyNotFoundException();
           return like;
        }

        public async Task<IEnumerable<Product>> GetLikedProducts()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Like>> GetLikes()
        {
            return await _context.Likes.ToListAsync();
        }

        public Task UnlikeProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserLikedProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}