namespace API.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        // Add other payment-related properties as needed

        // Foreign key relationship with Order
        public int OrderID { get; set; }
        public Order Order { get; set; }
    }
}