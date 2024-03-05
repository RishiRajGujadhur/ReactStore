using Store.Infrastructure.Entities.OrderAgrgregate;
using System.ComponentModel.DataAnnotations;

namespace Store.Infrastructure.Entities
{
    public class Logistics
    {
        [Key]
        public int LogisticsID { get; set; }

        [Required]
        public string ShipmentStatus { get; set; }

        // Foreign Key
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }
    }
}