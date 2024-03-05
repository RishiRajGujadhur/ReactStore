using System.ComponentModel.DataAnnotations;

namespace Store.Infrastructure.Entities
{
    public class ShippingMethod
    {
        [Key]
        public int ShippingMethodID { get; set; }

        [Required]
        public string MethodName { get; set; }

        [Required]
        public int EstimatedDeliveryTime { get; set; }
    }
}