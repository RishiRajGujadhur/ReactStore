using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Receipt : StoreEntry
    {
        [Key]
        public int Id { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } 
        public string Logo { get; set; } = "https://via.placeholder.com/150"; 
        public string Number { get; set; }  
        public string BottomNotice { get; set; } = "Thank you for your business.";
        // Foreign Key 
        public int UserId { get; set; }
        public User User { get; set; } 
        public List<OrderItem> OrderItems { get; set; }
        public ReceiptSettings Settings { get; set; } = new ReceiptSettings();
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
        public string Currency { get; set; } = "MUR";
        public string Locale { get; set; } = "en-US";
        public string TaxNotation { get; set; } = "vat";
        public double? MarginTop { get; set; } = 10;
        public double? MarginRight { get; set; } = 10;
        public double? MarginLeft { get; set; } = 10;
        public double? MarginBottom { get; set; } = 10;
        public string Format { get; set; } = "A4";
        public string Height { get; set; } = "210mm";
        public string Width { get; set; } = "297mm";
        public string Orientation { get; set; } = "portrait";
    }
}