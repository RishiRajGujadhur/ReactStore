using System.Security.Claims;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface IOrderBL
    {
      
    }

    public class OrderBL : IOrderBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public OrderBL(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        } 
    }
}