using Microsoft.AspNetCore.Http;
using BlakeBananaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System.Web.Helpers;
using BlakeBananaWeb.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BlakeBananaWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PurchaseDb _purchaseDb;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CartController(ApplicationDbContext context, PurchaseDb purchaseDb, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _purchaseDb = purchaseDb;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("cart") == null)
            {
                List<Item> cart = new List<Item>();
                ViewBag.Cart = cart;
            }
            else
            {
                var cartObj = JsonConvert.DeserializeObject<List<Item>>(HttpContext.Session.GetString("cart"));
                ViewBag.Cart = cartObj;
            }

            return View();
        }

        public ActionResult Buy(Product product)
        {
            if (HttpContext.Session.GetString("cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Products = product, Quantity = 1 });
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
            }
            else
            {
                var cartObj = JsonConvert.DeserializeObject<List<Item>>(HttpContext.Session.GetString("cart"));
                int index = isExist(product.Id);
                if (index != -1)
                {
                    cartObj[index].Quantity++;
                }
                else
                {
                    cartObj.Add(new Item { Products = product, Quantity = 1 });
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cartObj));
            }
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            var cartObj = JsonConvert.DeserializeObject<List<Item>>(HttpContext.Session.GetString("cart"));

            int index = isExist(id);
            cartObj.RemoveAt(index);
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cartObj));
            return RedirectToAction("Index");
        }

        public ActionResult ChangeQty(int id, int num)
        {
            var cartObj = JsonConvert.DeserializeObject<List<Item>>(HttpContext.Session.GetString("cart"));
            int index = isExist(id);
            if (index != -1)
            {
                var change = cartObj[index].Quantity + num;
                var prod = cartObj[index].Products;
                cartObj.RemoveAt(index);
                if (change != 0)
                {
                    cartObj.Add(new Item { Products = prod, Quantity = change });
                }
            }
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cartObj));
            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            var Id = "";
            if (HttpContext.Session.GetString("cart") == null)
            {
                List<Product> products = new List<Product>();
                products = _context.Product.ToList();
                Random random = new Random();
                var shuffled = products.OrderBy(p => random.Next()).ToList();
                shuffled.RemoveRange(4, (products.Count - 4));
                ViewBag.Products = shuffled;
                return View("Empty");
            }
            else
            {
                var cartObj = JsonConvert.DeserializeObject<List<Item>>(HttpContext.Session.GetString("cart"));
                if (_signInManager.IsSignedIn(User))
                {
                    Id = _userManager.GetUserId(User);
                }
                else
                {
                    Random random = new Random();
                    Id = random.Next().ToString();
                }
                foreach (Item item in cartObj)
                {
                    Purchase purchase = new Purchase()
                    {
                        ProductId = item.Products.Id,
                        Quantity = item.Quantity,
                        UserId = Id,
                        CreatedDate = DateTime.Now,
                    };
                    Purchase(purchase);
                }
                HttpContext.Session.Remove("cart");
                return View("Checkout");
            }

        }


        public int Purchase(Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _purchaseDb.Add(purchase);
                _purchaseDb.SaveChanges();
            }
            else
            {
                return 1;
            }
            return 0;
        }

        private int isExist(int id)
        {
            List<Item> cart = JsonConvert.DeserializeObject<List<Item>>(HttpContext.Session.GetString("cart"));
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Products.Id.Equals(id))
                    return i;
            return -1;
        }


    }
}
