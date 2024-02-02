using System.ComponentModel.DataAnnotations;
using API.Entities.OrderAggregate;

namespace API.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public long Subtotal { get; set; }
        public long DeliveryFee { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; } 
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        // New Relationship
        public virtual ICollection<ReturnRequest> ReturnRequests { get; set; }
        public ICollection<OrderDiscount> Discounts { get; set; }
        public ICollection<AdditionalDeliveryInfo> AdditionalDeliveryInfos { get; set; }

        public long GetTotal()
        {
            return Subtotal + DeliveryFee;
        }
    }
}