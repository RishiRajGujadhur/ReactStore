namespace Store.Infrastructure.Entities
{
    public class CustomerAddress : StoreEntry
    {
        public int CustomerAddressID { get; set; }
        public int CustomerID { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        // Foreign key relationship with Customer
        public Customer Customer { get; set; }
    }
}