using Store.Infrastructure.Entities.OrderAgrgregate;

namespace Store.Infrastructure.Entities
{
    public class AdditionalDeliveryInfo
    {
        public int AdditionalDeliveryInfoID { get; set; }
        public int OrderID { get; set; }
        public string DeliveryInstructions { get; set; }

        // Navigation property for Order
        public Order Order { get; set; }
    }
}