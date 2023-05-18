using BlakeBananaWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlakeBananaWeb.Data
{
    public class PurchaseDb : IdentityDbContext
    {
        public PurchaseDb(DbContextOptions<PurchaseDb> options) : base(options)
        {

        }

        public DbSet<Purchase> Purchase { get; set; }
    }
}
