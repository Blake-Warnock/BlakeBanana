using Microsoft.AspNetCore;
using System.ComponentModel.DataAnnotations;

namespace BlakeBananaWeb.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public string ImagePath { get; set; }
        public string Category { get; set; }

    }

}

