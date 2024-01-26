using API.Data;
using API.Entities;
using API.Extensions;
using API.RequestHelpers;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public CommentController(StoreContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        // POST: api/comments
        [HttpPost]
        public async Task<ActionResult> CreateComment([FromBody] CommentCreateDto commentDto)
        {
            try
            {
                var userId = await GetUserId();

                var comment = _mapper.Map<Comment>(commentDto);
                comment.UserId = userId;
                comment.CreatedAt = DateTime.UtcNow;

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                var commentResponse = _mapper.Map<CommentDto>(comment);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, ex.InnerException);
            }
        }

        // GET: api/comments/{id}
        [HttpGet("{productId}")]
        public async Task<ActionResult<List<CommentDto>>> GetCommentsByProduct(int productId)
        {
            var comments = await _context.Comments
                .Where(c => c.ProductId == productId)
                .ToListAsync();

            var commentDtos = _mapper.Map<List<CommentDto>>(comments);

            return commentDtos;
        }


        // GET: api/comments/{id}
        [HttpGet("list")]
        public async Task<ActionResult<List<Comment>>> GetCommentsByProductPaged([FromQuery] CommentDto commentDto)
        {
            var comments =  _context.Comments
                .Where(c => c.ProductId == commentDto.ProductId); 

             var query = comments
                .AsQueryable();

            var commentPagedList =
                await PagedList<Comment>.ToPagedList(query, commentDto.PageNumber, commentDto.PageSize);

            Response.AddPaginationHeader(commentPagedList.MetaData);
            return commentPagedList;
        }

        // PUT: api/comments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentUpdateDto commentDto)
        {
            try
            {
                var userId = await GetUserId();

                var comment = await _context.Comments
                    .Where(c => c.Id == id && c.UserId == userId)
                    .FirstOrDefaultAsync();

                if (comment == null)
                {
                    return NotFound();
                }

                _mapper.Map(commentDto, comment);

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, ex.InnerException);
            }
        }

        // DELETE: api/comments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var userId = await GetUserId();

                var comment = await _context.Comments
                    .Where(c => c.Id == id && c.UserId == userId)
                    .FirstOrDefaultAsync();

                if (comment == null)
                {
                    return NotFound();
                }

                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, ex.InnerException);
            }
        }

        private async Task<int> GetUserId()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return user.Id;
        }
    }
}
