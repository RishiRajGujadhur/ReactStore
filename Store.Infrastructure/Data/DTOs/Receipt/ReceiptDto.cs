namespace Store.Infrastructure.Data.DTOs.Receipt;

public class ReceiptDto
{
    public int Id { get; set; }
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; }
    public string Logo { get; set; } = "https://via.placeholder.com/150";
    public string Number { get; set; }
    public int UserId { get; set; }
}