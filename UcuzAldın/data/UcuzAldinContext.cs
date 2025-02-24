using Microsoft.EntityFrameworkCore;

public class UcuzAldinContext : DbContext
{
    public UcuzAldinContext(DbContextOptions<UcuzAldinContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(c => c.SubCategories)
            .WithOne(c => c.ParentCategory)
            .HasForeignKey(c => c.ParentCategoryId);
    }
}