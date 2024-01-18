using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        // New Relationship
        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        // Navigation property for addresses
        public ICollection<CustomerAddress> Addresses { get; set; }

        // Navigation property for wishlists
        public Wishlist Wishlist { get; set; } // Assuming a one-to-one relationship

    }
}