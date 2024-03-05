using System.ComponentModel.DataAnnotations;

namespace Store.Infrastructure.Entities
{
    public class Comment : StoreEntry
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key relationship with Product
        public int ProductId { get; set; }
        public Product Product { get; set; }

        // Foreign key relationship with User (assuming you have a User model)
        public int UserId { get; set; }
        public User User { get; set; }
    }
}