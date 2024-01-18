using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Warehouse
    {
        [Key]
        public int WarehouseID { get; set; }

        [Required]
        public string WarehouseName { get; set; }

        [Required]
        public string Location { get; set; }
    }
}