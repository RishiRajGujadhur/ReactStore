using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data;
using Store.Infrastructure.Data.DTOs.Basket;
using Store.Infrastructure.Entities;

namespace Store.API.Extentions;

public static class BasketExtensions
{
    public static BasketDto MapBasketToDto(this Basket basket)
    {
        return new BasketDto
        {
            Id = basket.Id,
            BuyerId = basket.BuyerId,
            // PaymentIntentId = basket.PaymentIntentId,
            // ClientSecret = basket.ClientSecret,
            Items = basket.Items.Select(item => new BasketItemDto
            {
                ProductId = item.ProductId,
                Name = item.Product.Name,
                Price = item.Product.Price,
                PictureUrl = item.Product.PictureUrl,
                Type = item.Product.Type,
                Brand = item.Product.Brand,
                Quantity = item.Quantity
            }).ToList()
        };
    }

    public static IQueryable<Basket> RetrieveBasketWithItems(this IQueryable<Basket> query, string buyerId)
    {
        return query
            .Include(i => i.Items)
            .ThenInclude(p => p.Product)
            .Where(b => b.BuyerId == buyerId);
    }

    public static async Task<Basket> RetrieveBasket(string buyerId, HttpResponse response, StoreContext _context)
    {
        if (string.IsNullOrEmpty(buyerId))
        {
            response.Cookies.Delete("buyerId");
            return null;
        }

        return await _context.Baskets
            .Include(i => i.Items)
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(basket => basket.BuyerId == buyerId);
    }
}