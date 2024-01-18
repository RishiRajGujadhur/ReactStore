namespace API.Entities
{
    public class Wishlist
    {
        public int WishlistID { get; set; }
        public int CustomerID { get; set; }

        // Navigation property for Customer
        public Customer Customer { get; set; }

        // Navigation property for products in the wishlist
        public ICollection<Product> Products { get; set; }

        // Collection navigation property for WishlistItems
        public ICollection<WishlistItem> WishlistItems { get; set; }
    }
}