using Store.Infrastructure.Entities.OrderAgrgregate;

namespace Store.Infrastructure.Data.DTOs.Order;

public class CreateOrderDto
{
    public bool SaveAddress { get; set; }
    public ShippingAddress ShippingAddress { get; set; }
    public string PaymentMethod { get; set; }
}