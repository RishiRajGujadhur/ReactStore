using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    // OrderItem Table
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }

        public int Quantity { get; set; }

        public decimal Subtotal { get; set; }

        // Foreign Keys
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int InvoiceId { get; set; }
        
        public virtual Invoice Invoice { get; set; }

        public int ReceiptId { get; set; } 
        public virtual Receipt Receipt { get; set; }

    }
}