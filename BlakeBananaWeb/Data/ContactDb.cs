
using BlakeBananaWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlakeBananaWeb.Data
{
    public class ContactDb : IdentityDbContext
    {
        public ContactDb(DbContextOptions<ContactDb> options) : base(options)
        {

        }

        public DbSet<Contact> Contact { get; set; }
    }
}
