using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using BlakeBananaWeb.Data;
using BlakeBananaWeb.Models;

namespace BlakeBananaWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Product != null ?
                        View(await _context.Product.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Products'  is null.");
        }
        public async Task<IActionResult> ViewProduct(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var products = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }
    }
}
