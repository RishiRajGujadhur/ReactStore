using API.Entities;

namespace API.DTOs.Invoice
{
    public class InvoiceDetailsDto
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; } = DateTime.UtcNow.AddDays(14);
        public string Logo { get; set; } = "https://via.placeholder.com/150";
        public string Number { get; set; }
        public int UserId { get; set; }
        public string BottomNotice { get; set; } = "Thank you for your business.";
        public List<ProductItemsDto> Products { get; set; }
        public InvoiceSender Sender { get; set; }
        public CustomerDto Customer { get; set; }
        public InvoiceSettings Settings { get; set; } = new InvoiceSettings();
    }

    public class ProductItemsDto
    {
        public int Quantity { get; set; }
        public string Description { get; set; }
        public decimal TaxRate { get; set; }
        public decimal Price { get; set; }
    }

    public class CustomerDto
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    } 
}