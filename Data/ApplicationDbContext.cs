using Microsoft.EntityFrameworkCore;
using DelaCruz_Midterm_Store.Models;

namespace DelaCruz_Midterm_Store.Data
{
    // ✿ Database context ✿ (•ᴗ•)
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}