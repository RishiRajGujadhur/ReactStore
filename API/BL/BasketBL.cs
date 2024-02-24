using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BL
{
    public interface IBasketBL
    {
        Task<BasketDto> GetBasket();
        Task AddItemToBasket(int productId, int quantity = 1);
        Task RemoveBasketItem(int productId, int quantity = 1);
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

        public Task<BasketDto> GetBasket()
        {
            throw new NotImplementedException();
        }

        public Task AddItemToBasket(int productId, int quantity = 1)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBasketItem(int productId, int quantity = 1)
        {
            throw new NotImplementedException();
        }
    }
}