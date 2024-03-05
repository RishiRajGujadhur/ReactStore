namespace Store.Infrastructure.Entities
{
    public class Product : StoreEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string PictureUrl { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public int QuantityInStock { get; set; }

        // New Relationship
        public virtual ICollection<Review> Reviews { get; set; }
        public ICollection<CollectionList> CollectionLists { get; set; }
        // Navigation property for Likes
        public virtual ICollection<Like> Likes { get; set; }
        public string PublicId { get; set; }
    }
}