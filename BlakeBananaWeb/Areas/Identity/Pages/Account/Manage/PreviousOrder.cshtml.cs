// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.Threading.Tasks;
using BlakeBananaWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BlakeBananaWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BlakeBananaWeb.Areas.Identity.Pages.Account.Manage
{
    public class PreviousOrderModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly PurchaseDb _context;
        private readonly ApplicationDbContext _Pcontext;
        List<Item> purchaseList = new List<Item>();

        public PreviousOrderModel(
            UserManager<IdentityUser> userManager,
            PurchaseDb context,
            ApplicationDbContext pcontext)
        {
            _userManager = userManager;
            _context = context;
            _Pcontext = pcontext;
        }


        public async Task<IActionResult> OnGet()
        {
            var user = _userManager.GetUserId(User);

            if (_context != null)
            {
                foreach (Purchase purchase in _context.Purchase.ToList())
                {
                    if (purchase.UserId == user)
                    {
                        foreach (Product product in _Pcontext.Product.ToList())
                        {
                            if (purchase.ProductId == product.Id)
                            {
                                purchaseList.Add(new Item
                                {
                                    Products = product,
                                    Quantity = purchase.Quantity,
                                });
                            }
                        }
                    }
                }
                ViewData["list"] = purchaseList;
            }
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }
    }


}
