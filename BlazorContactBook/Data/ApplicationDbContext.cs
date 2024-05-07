using BlazorContactBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorContactBook.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<ImageUpload> Images { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Category> Categories { get; set; }

    }
}
