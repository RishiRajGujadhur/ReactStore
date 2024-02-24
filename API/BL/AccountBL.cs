using System.Security.Claims;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface IAccountBL
    {
      
    }

    public class AccountBL : IAccountBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public AccountBL(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        } 
    }
}