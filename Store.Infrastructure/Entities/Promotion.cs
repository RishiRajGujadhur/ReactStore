using System.ComponentModel.DataAnnotations;

namespace Store.Infrastructure.Entities
{
    public class Promotion
    {
        [Key]
        public int PromotionID { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public decimal DiscountPercentage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}