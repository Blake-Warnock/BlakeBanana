using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;
using Microsoft.EntityFrameworkCore;
using BlakeBananaWeb.Data;
using BlakeBananaWeb.Models;
using RestSharp;

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
            return _context.Products != null ?
                        View(await _context.Products.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Products'  is null.");
        }
        public IActionResult Category()
        {
            IEnumerable<Products> products = _context.Products;
            return View(products);
        }
        public async Task<IActionResult> ViewProduct(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }
        public async Task<IActionResult> ViewCategory(string? passed)
        {
            var category = passed;
            IEnumerable<Products> products = _context.Products;
            ViewBag.Category = category;
            ViewData["products"] = products;
            return View();
        }
    }
}
