using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Receipt
    {
        [Key]
        public int Id { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }

        [Required]
        public DateTime DueDate { get; set; } 

        public string Logo { get; set; }

        public string Number { get; set; }
        public string Date { get; set; }

        public string BottomNotice { get; set; }
        // Foreign Key

        public int UserId { get; set; }
        public User User { get; set; } 
        public List<OrderItem> OrderItems { get; set; }
        public ReceiptSettings Settings { get; set; }
        public Customer Customer { get; set; }
        public ReceiptSender Sender { get; set; }
    }

    public class ReceiptSender
    {
        [Key]
        public int Id { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class ReceiptSettings
    {
        [Key]
        public int Id { get; set; }
        public string Currency { get; set; }
        public string Locale { get; set; }
        public string TaxNotation { get; set; }
        public double? MarginTop { get; set; }
        public double? MarginRight { get; set; }
        public double? MarginLeft { get; set; }
        public double? MarginBottom { get; set; }
        public string Format { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Orientation { get; set; }
    }
}