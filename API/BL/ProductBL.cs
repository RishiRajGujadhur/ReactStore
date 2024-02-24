using System.Security.Claims;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface IProductBL
    {
      
    }

    public class ProductBL : IProductBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public ProductBL(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        } 
    }
}