namespace API.Entities
{
    public class WishlistItem
    {
        public int WishlistItemID { get; set; }
        public int ProductID { get; set; }

        // Foreign key relationship with Wishlist
        public int WishlistID { get; set; }
        public Wishlist Wishlist { get; set; }

        // Navigation property for Product
        public Product Product { get; set; }
    }
}