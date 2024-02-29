using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Customer : StoreEntry
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

        public string Company { get; set; } = "";
        public string Zip { get; set; } = "";
        public string City { get; set; } = "";
        public string Country { get; set; } = "";

        // Navigation property for linking Customer to User (one-to-one)
        public virtual User User { get; set; }

    }
}