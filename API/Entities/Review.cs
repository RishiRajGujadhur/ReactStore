using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Review : StoreEntry
    {

        [Key]
        public int ReviewID { get; set; }

        [Required]
        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime DatePosted { get; set; }

        // Foreign Keys
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
    }
}