using Microsoft.AspNetCore.Mvc;
using Store.API.BL;

namespace Store.API.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentBL _commentBL;
        public CommentController(ICommentBL commentBL)
        {
            _commentBL = commentBL;
        }

        // POST: api/comments
        [HttpPost]
        public async Task<ActionResult> CreateComment([FromBody] CommentCreateDto commentDto)
        {
            try
            {
                await _commentBL.CreateComment(commentDto, User);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, ex.InnerException);
            }
        }

        // GET: api/comments/{id}
        [HttpGet("list")]
        public async Task<ActionResult<List<GetCommentDto>>> GetCommentsByProductPaged([FromQuery] CommentDto commentDto)
        {
            try
            {
                var comments = await _commentBL.GetCommentsByProductPaged(commentDto, Response);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, ex.InnerException);
            }
        }

        // PUT: api/comments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentUpdateDto commentDto)
        {
            try
            {
                await _commentBL.UpdateComment(id, commentDto, User);
                return Ok();
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
                await _commentBL.DeleteComment(id, User);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, ex.InnerException);
            }
        }
    }
}
