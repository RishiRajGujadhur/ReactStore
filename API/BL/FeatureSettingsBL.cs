using System.Security.Claims;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface IFeatureSettingsBL
    {
      
    }

    public class FeatureSettingsBL : IFeatureSettingsBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public FeatureSettingsBL(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        } 
    }
}