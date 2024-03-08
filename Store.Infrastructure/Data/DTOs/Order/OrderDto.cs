using Store.Infrastructure.Entities.OrderAgrgregate;

namespace Store.Infrastructure.Data.DTOs.Order;

public class OrderDto
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public ShippingAddress ShippingAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
    public long Subtotal { get; set; }
    public long DeliveryFee { get; set; }
    public string OrderStatus { get; set; }
    public string PaymentMethod { get; set; } = "Cash on Delivery";
    public long Total { get; set; }
}