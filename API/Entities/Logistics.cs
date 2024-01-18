

using System.ComponentModel.DataAnnotations;

namespace API.Entities
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