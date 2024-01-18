using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public bool IsRead { get; set; }

        // Foreign Key
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
    }
}