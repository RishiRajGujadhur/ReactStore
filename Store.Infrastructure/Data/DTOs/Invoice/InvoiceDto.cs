using Store.Infrastructure.RequestHelpers;

namespace Store.Infrastructure.Data.DTOs.Invoice;

public class InvoiceDto : MetaData
{
    public int Id { get; set; }
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    public DateTime PaymentDueDate { get; set; } = DateTime.UtcNow.AddDays(14);
    public string Logo { get; set; } = "https://via.placeholder.com/150";
    public string Number { get; set; }
    public int UserId { get; set; }
}