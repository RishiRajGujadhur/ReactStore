using System.ComponentModel.DataAnnotations;

namespace Store.Infrastructure.Entities
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        // Navigation property for User
        public User User { get; set; }

        // Navigation property for Product
        public Product Product { get; set; }
    }
}
