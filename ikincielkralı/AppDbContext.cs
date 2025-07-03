using Microsoft.EntityFrameworkCore;
using ikincielkralı.Models;

namespace ikincielkralı
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Listing> Listings { get; set; }
    }
}
