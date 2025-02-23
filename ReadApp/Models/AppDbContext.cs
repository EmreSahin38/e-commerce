using Microsoft.EntityFrameworkCore;

namespace ReadApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Roman" },
                new Category { CategoryId = 2, Name = "Hikaye" },
                new Category { CategoryId = 3, Name = "Biyografi" },
                new Category { CategoryId = 4, Name = "Söylev" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Son Ayı", Pages = 250, IsActive = true, CategoryId = 2 },
                new Product { ProductId = 2, Name = "Tarık Buğra'nın Roman Dünyası", Pages = 350, IsActive = true, CategoryId = 1 },
                new Product { ProductId = 3, Name = "Cadı", Pages = 342, IsActive = true, CategoryId = 1 },
                new Product { ProductId = 4, Name = "Ateşle Oynayan Kız", Pages = 350, IsActive = true, CategoryId = 1 },
                new Product { ProductId = 5, Name = "Ejderha Dövmeli Kız", Pages = 380, IsActive = true, CategoryId = 1 },
                new Product { ProductId = 6, Name = "Arı Kovanına Çomak Sokan Kız", Pages = 270, IsActive = true, CategoryId = 1 },
                new Product { ProductId = 7, Name = "Kız Kulesindeki Kızılderili", Pages = 250, IsActive = true, CategoryId = 1 },
                new Product { ProductId = 8, Name = "Şeker Portakalı", Pages = 269, IsActive = true, CategoryId = 1 },
                new Product { ProductId = 9, Name = "Hayvan Çiftliği", Pages = 452, IsActive = true, CategoryId = 1 },
                new Product { ProductId = 10, Name = "Beyaz Diş", Pages = 290, IsActive = true, CategoryId = 1 },
                new Product { ProductId = 11, Name = "Küçük Prens", Pages = 370, IsActive = true, CategoryId = 2 },
                new Product { ProductId = 12, Name = "Silmarillion", Pages = 700, IsActive = true, CategoryId = 1 },
                new Product { ProductId = 13, Name = "La Fontaine Masalları", Pages = 150, IsActive = true, CategoryId = 2 },
                new Product { ProductId = 14, Name = "Anne Frank'in Hatıra Defteri", Pages = 284, IsActive = true, CategoryId = 3 },
                new Product { ProductId = 15, Name = "Nutuk", Pages = 543, IsActive = true, CategoryId = 4 }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "admin", Password = "password", Role = "Admin" },
                new User { UserId = 2, Username = "Admin", Password = "Admin38", Role = "Admin" }
            );
        }
    }
}