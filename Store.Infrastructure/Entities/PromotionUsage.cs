using Store.Infrastructure.Entities.OrderAgrgregate;
using System.ComponentModel.DataAnnotations;

namespace Store.Infrastructure.Entities
{
    public class PromotionUsage
    {
        [Key]
        public int UsageID { get; set; }

        // Foreign Keys
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public int PromotionID { get; set; }
        public virtual Promotion Promotion { get; set; }

        public int OrderID { get; set; }
        public virtual Order Order { get; set; }
    }
}