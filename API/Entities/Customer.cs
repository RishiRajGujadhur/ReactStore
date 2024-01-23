using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        // New Relationship
        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        // Navigation property for wishlists
        public Wishlist Wishlist { get; set; } // Assuming a one-to-one relationship

        // Navigation property for linking Customer to User (one-to-one)
        public virtual User User { get; set; }

    }
}