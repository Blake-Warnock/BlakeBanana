using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlakeBananaWeb.Data;
using BlakeBananaWeb.Models;

namespace BlakeBananaWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public IActionResult Index()
        {
            IEnumerable<Product> ProductList = _context.Product;
            return View(ProductList);
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Admin/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var products = _context.Product.Find(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product products)
        {

            if (ModelState.IsValid)
            {

                _context.Product.Update(products);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var products = await _context.Product.FindAsync(id);
            if (products != null)
            {
                _context.Product.Remove(products);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
