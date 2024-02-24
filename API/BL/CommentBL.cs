using System.Security.Claims;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface ICommentBL
    {
      
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
    }
}