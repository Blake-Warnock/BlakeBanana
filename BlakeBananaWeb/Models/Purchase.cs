using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BlakeBananaWeb.Models
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
