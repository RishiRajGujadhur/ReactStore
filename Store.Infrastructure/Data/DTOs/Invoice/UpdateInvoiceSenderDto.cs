namespace Store.Infrastructure.Data.DTOs.Invoice
{
    public class UpdateInvoiceSenderDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
    }
}