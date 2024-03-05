using Store.Infrastructure.Entities.OrderAgrgregate;
using System.ComponentModel.DataAnnotations;

namespace Store.Infrastructure.Entities
{
    public class Invoice : StoreEntry
    {
        [Key]
        public int Id { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DueDate { get; set; } = DateTime.UtcNow.AddDays(14);

        [Required]
        public string Logo { get; set; } = "https://via.placeholder.com/150";
        public string Number { get; set; }
        public string BottomNotice { get; set; } = "Thank you for your business.";
        // Foreign Key
        public int UserId { get; set; }
        public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public InvoiceSettings Settings { get; set; } = new InvoiceSettings();
        public InvoiceSender Sender { get; set; }
    }

    public class InvoiceSender : StoreEntry
    {
        [Key]
        public int Id { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int UserId { get; set; }
    }

    public class InvoiceSettings : StoreEntry
    {
        [Key]
        public int Id { get; set; }
        public string Currency { get; set; } = "MUR";
        public string BottomNotice { get; set; } = "Bottom Notice";
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