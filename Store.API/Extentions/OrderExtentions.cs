using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data.DTOs.Order;
using Store.Infrastructure.Entities.OrderAgrgregate;

namespace Store.API.Extentions;

public static class OrderExtensions
{
    public static IQueryable<OrderDto> ProjectOrderToOrderDto(this IQueryable<Order> query)
    {
        return query
            .Select(order => new OrderDto
            {
                Id = order.Id,
                BuyerId = order.BuyerId,
                OrderDate = order.OrderDate,
                ShippingAddress = order.ShippingAddress,
                DeliveryFee = order.DeliveryFee,
                Subtotal = order.Subtotal,
                OrderStatus = order.OrderStatus.ToString(),
                Total = order.GetTotal(),
                PaymentMethod = order.PaymentMethod,
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    ProductId = item.ItemOrdered.ProductId,
                    Name = item.ItemOrdered.Name,
                    PictureUrl = item.ItemOrdered.PictureUrl,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            }).AsNoTracking();
    }
}