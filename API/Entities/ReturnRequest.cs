using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class ReturnRequest
    {
        [Key]
        public int ReturnRequestID { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string Status { get; set; }

        // Foreign Keys
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }

        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
    }
}