using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        // Navigation property for payments
        public ICollection<Payment> Payments { get; set; }

        // Foreign Key
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        // New Relationship
        public virtual ICollection<ReturnRequest> ReturnRequests { get; set; }
        public ICollection<OrderDiscount> Discounts { get; set; }
        public ICollection<AdditionalDeliveryInfo> AdditionalDeliveryInfos { get; set; }
    }
}