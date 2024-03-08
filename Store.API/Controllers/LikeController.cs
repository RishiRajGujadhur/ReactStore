using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.API.BL;
using Store.Infrastructure.Entities;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Member")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeBL _likeBL;

        public LikeController(ILikeBL likeBL)
        {
            _likeBL = likeBL;
        }

        // GET: api/like
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Like>>> GetLikes()
        {
            var likes = await _likeBL.GetLikes();
            return Ok(likes);
        }

        // GET: api/like/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Like>> GetLike(int id)
        {
            return await _likeBL.GetLike(id);
        }

        [HttpGet("user-liked")]
        public async Task<ActionResult<bool>> UserLikedProduct(int productId)
        {
            // Validate the DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool result = await _likeBL.UserLikedProduct(productId, User);
            return Ok(result);
        }

        [HttpDelete("unlike")]
        public async Task<ActionResult> UnlikeProduct(int productId)
        {
            // Validate the DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _likeBL.UnlikeProduct(productId, User);
            return NoContent();
        }

        // POST: api/like
        [HttpGet("like")]
        public async Task<ActionResult> CreateLike(int productId)
        {
            // Validate the DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _likeBL.CreateLike(productId, User);
            return NoContent();
        }

        // GET: api/like/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetLikedProducts()
        {
            var products = await _likeBL.GetLikedProducts(User);
            return Ok(products);
        }
    }
}
