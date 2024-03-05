using System.ComponentModel.DataAnnotations;

namespace Store.Infrastructure.Entities
{
    public class CollectionListItem
    {
        [Key]
        public int CollectionListItemID { get; set; }

        // Foreign key relationship with Collectionlist
        public int CollectionListID { get; set; }
        public CollectionList CollectionList { get; set; }

        // Navigation property for Product
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}