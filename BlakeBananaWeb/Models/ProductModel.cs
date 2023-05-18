using BlakeBananaWeb.Data;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace BlakeBananaWeb.Models
{
    public class ProductModel
    {

        private readonly ApplicationDbContext _context;
        private List<Product> products;

        public ProductModel(ApplicationDbContext context)
        {
            _context = context;

            products = new List<Product>();

            foreach (var product in _context.Product)
            {
                products.Add(product);
            };
        }

        public Product find(string id)
        {
            return this.products.Single(p => p.Id.Equals(id));
        }

    }
}
