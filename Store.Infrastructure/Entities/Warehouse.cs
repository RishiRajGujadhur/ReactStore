using System.ComponentModel.DataAnnotations;

namespace Store.Infrastructure.Entities
{
    public class Warehouse : StoreEntry
    {
        [Key]
        public int WarehouseID { get; set; }

        [Required]
        public string WarehouseName { get; set; }

        [Required]
        public string Location { get; set; }
    }
}