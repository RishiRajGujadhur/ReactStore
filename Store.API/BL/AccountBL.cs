using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.API.Extentions;
using Store.API.Integrations.Services;
using Store.Infrastructure.Data;
using Store.Infrastructure.Data.DTOs.Account;
using Store.Infrastructure.Entities;

namespace Store.API.BL
{
    public interface IAccountBL
    {
        Task<UserAddress> GetSavedAddress(ClaimsPrincipal User);
        Task<List<User>> GetAllUsers();
        Task<UserDto> GetCurrentUser(ClaimsPrincipal User, HttpResponse response);
    }

    public class AccountBL : IAccountBL
    {
        private readonly StoreContext _context;

        private readonly TokenService _tokenService;
        public UserManager<User> _userManager { get; }

        public AccountBL(StoreContext context, UserManager<User> userManager, TokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<UserAddress> GetSavedAddress(ClaimsPrincipal User)
        {
            return await _userManager.Users
                .Where(x => x.UserName == User.Identity.Name)
                .Select(user => user.Address)
                .FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userManager.Users.Include
               (x => x.Address)
               .ToListAsync();
        }

        public async Task<UserDto> GetCurrentUser(ClaimsPrincipal User, HttpResponse response)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var userBasket = await BasketExtensions.RetrieveBasket(User.Identity.Name, response, _context);

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                Basket = userBasket?.MapBasketToDto()
            };
        }
    }
}