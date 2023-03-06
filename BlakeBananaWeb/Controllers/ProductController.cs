using BlakeBananaWeb.Data;
using BlakeBananaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlakeBananaWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Products> products = _context.Products;
            return View(products);
        }
    }
}
