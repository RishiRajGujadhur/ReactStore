

using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Inventory : StoreEntry
    {
        [Key]
        public int InventoryID { get; set; }

        // Foreign Keys
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int WarehouseID { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        [Required]
        public int StockQuantity { get; set; }
    }
}