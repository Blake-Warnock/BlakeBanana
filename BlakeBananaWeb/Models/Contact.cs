using System.ComponentModel.DataAnnotations;

namespace BlakeBananaWeb.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        public string purpose { get; set; }
        public string message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
