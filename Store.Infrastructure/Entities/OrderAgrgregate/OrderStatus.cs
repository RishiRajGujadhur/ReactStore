namespace Store.Infrastructure.Entities.OrderAgrgregate;

public enum OrderStatus
{
    Pending,
    PaymentReceived,
    Delivered,
    PaymentFailed
}