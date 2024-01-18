namespace API.Entities
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } // Assuming a relationship with products
    }
}