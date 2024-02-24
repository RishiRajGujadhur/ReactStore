using System.Security.Claims;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface ICommentBL
    {
        Task CreateComment(CommentCreateDto commentDto);
        Task<List<GetCommentDto>> GetCommentsByProductPaged(CommentDto commentDto);
        Task UpdateComment(int id, CommentUpdateDto commentDto);
        Task DeleteComment(int id);
    }

    public class CommentBL : ICommentBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public CommentBL(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task CreateComment(CommentCreateDto commentDto)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetCommentDto>> GetCommentsByProductPaged(CommentDto commentDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateComment(int id, CommentUpdateDto commentDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteComment(int id)
        {
            throw new NotImplementedException();
        }
    }
}