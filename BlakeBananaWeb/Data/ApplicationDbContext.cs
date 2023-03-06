
using BlakeBananaWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BlakeBananaWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Products> Products { get; set; }
    }
}
