using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface IBasketBL
    {
        Task<BasketDto> GetBasket(ClaimsPrincipal User, HttpResponse response);
        Task<(bool,Basket)> AddItemToBasket(int productId, ClaimsPrincipal User, HttpResponse response, int quantity = 1);
        Task<bool> RemoveBasketItem(int productId, ClaimsPrincipal User, HttpResponse response, int quantity = 1);
    }

    public class BasketBL : IBasketBL
    {
        private readonly StoreContext _context;
        public UserManager<User> _userManager { get; }

        public BasketBL(StoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<BasketDto> GetBasket(ClaimsPrincipal User, HttpResponse Response)
        {
            var basket = await RetrieveBasket(GetBuyerId(User), Response) ?? throw new Exception("Basket not found");
            return basket.MapBasketToDto();
        }

        public async Task<(bool,Basket)> AddItemToBasket(int productId, ClaimsPrincipal User, HttpResponse response, int quantity = 1)
        {
            var basket = await RetrieveBasket(GetBuyerId(User), response);

            if (basket == null) basket = CreateBasket(User, response);

            var product = await _context.Products.FindAsync(productId);

            if (product == null) throw new Exception("Product not found");

            basket.AddItem(product, quantity);

            var result = await _context.SaveChangesAsync() > 0;
            return (result, basket);
        }

        public async Task<bool> RemoveBasketItem(int productId, ClaimsPrincipal User, HttpResponse Response, int quantity = 1)
        {
            var basket = await RetrieveBasket(GetBuyerId(User), Response) ?? throw new Exception("Basket not found");

            basket.RemoveItem(productId, quantity);

            var result = await _context.SaveChangesAsync() > 0;

            return result;
        }

        #region Private Methods
        private async Task<Basket> RetrieveBasket(string buyerId, HttpResponse Response)
        {
            if (string.IsNullOrEmpty(buyerId))
            {
                Response.Cookies.Delete("buyerId");
                return null;
            }

            return await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(basket => basket.BuyerId == buyerId);
        }

        private string GetBuyerId(ClaimsPrincipal User, string buyerId = null)
        {
            return User.Identity?.Name ?? buyerId;
            //Request.Cookies["buyerId"];
        }

        private Basket CreateBasket(ClaimsPrincipal User, HttpResponse response)
        {
            var buyerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerId))
            {
                buyerId = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
                response.Cookies.Append("buyerId", buyerId, cookieOptions);
            }

            var basket = new Basket { BuyerId = buyerId };
            _context.Baskets.Add(basket);
            return basket;
        }
        #endregion
    }
}