namespace API.Entities
{
    public class OrderDiscount
    {
        public int OrderDiscountID { get; set; }
        public string DiscountCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; } // Assuming a relationship with order
    }
}