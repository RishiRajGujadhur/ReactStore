using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.API.Extentions;
using Store.API.RequestHelpers;
using Store.Infrastructure.Data;
using Store.Infrastructure.Entities;

namespace Store.API.BL
{
    public interface ICommentBL
    {
        Task CreateComment(CommentCreateDto commentDto, ClaimsPrincipal user);
        Task<List<GetCommentDto>> GetCommentsByProductPaged(CommentDto commentDto, HttpResponse response);
        Task UpdateComment(int id, CommentUpdateDto commentDto, ClaimsPrincipal user);
        Task DeleteComment(int id, ClaimsPrincipal user);
    }

    public class CommentBL : ICommentBL
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public CommentBL(StoreContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task CreateComment(CommentCreateDto commentDto, ClaimsPrincipal user)
        {
            try
            {
                var userId = await GetUserId(user);

                var comment = _mapper.Map<Comment>(commentDto);
                comment.UserId = userId;
                comment.CreatedAt = DateTime.UtcNow;
                comment.CreatedAtTimestamp = DateTime.UtcNow;
                comment.CreatedByUserName = user.Identity.Name;
                comment.CreatedByUserId = userId;

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                var commentResponse = _mapper.Map<CommentDto>(comment);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task<List<GetCommentDto>> GetCommentsByProductPaged(CommentDto commentDto, HttpResponse response)
        {
            var query = _context.Comments
              .Include(c => c.User)
              .Where(c => c.ProductId == commentDto.ProductId)
              .OrderByDescending(c => c.Id)
              .AsQueryable();

            var commentPagedList = await PagedList<Comment>.ToPagedList(query, commentDto.PageNumber, commentDto.PageSize);
            var commentsWithUsername = commentPagedList.Select(c => new GetCommentDto
            {
                Id = c.Id,
                Text = c.Text,
                CreatedAt = c.CreatedAt,
                ProductId = c.ProductId,
                Username = c.User?.UserName
            }).ToList();

            response.AddPaginationHeader(commentPagedList.MetaData);
            return commentsWithUsername;
        }

        public async Task UpdateComment(int id, CommentUpdateDto commentDto, ClaimsPrincipal user)
        {
            try
            {
                var userId = await GetUserId(user);

                var comment = await _context.Comments
                    .Where(c => c.Id == id && c.UserId == userId)
                    .FirstOrDefaultAsync() ?? throw new Exception("Comment not found");
                _mapper.Map(commentDto, comment);
                comment.LastModifiedTimestamp = DateTime.UtcNow;
                comment.LastModifiedUserId = userId;
                comment.LastModifiedUserName = user.Identity.Name;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task DeleteComment(int id, ClaimsPrincipal user)
        {
            try
            {
                var userId = await GetUserId(user);

                var comment = await _context.Comments
                    .Where(c => c.Id == id && c.UserId == userId)
                    .FirstOrDefaultAsync() ?? throw new Exception("Comment not found");

                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception(ex.InnerException.Message);
            }
        }

        #region Private Methods
        // Todo: Move to static method, extentions: User.GetUserId()
        private async Task<int> GetUserId(ClaimsPrincipal User)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return user.Id;
        }
        #endregion
    }
}