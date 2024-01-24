using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class CollectionList
    {
        [Key]
        public int CollectionListID { get; set; }
        public int UserID { get; set; }

        // Navigation property for User
        public User User { get; set; }

        // Navigation property for products in the Collectionlist
        public ICollection<Product> Products { get; set; }

        // Collection navigation property for CollectionlistItems
        public ICollection<CollectionListItem> CollectionListItems { get; set; }
    }
}