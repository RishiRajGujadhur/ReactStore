using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        // Foreign Key
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }
    }
}