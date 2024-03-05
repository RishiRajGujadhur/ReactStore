using System.ComponentModel.DataAnnotations;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Entities.OrderAgrgregate
{
    // OrderItem Table
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public long Price { get; set; }
        public ProductItemOrdered ItemOrdered { get; set; }
        public int Quantity { get; set; }
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public int? ReceiptId { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}