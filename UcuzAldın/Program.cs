// filepath: /c:/code/C#/UcuzAldın/Program.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext to the services
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<UcuzAldinContext>(options =>
    options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<UcuzAldinContext>();
    context.Database.Migrate();
    SeedData(context);
}

app.Run();

public partial class Program
{
    private static void SeedData(UcuzAldinContext context)
    {
        if (!context.Categories.Any())
        {
            var elektronik = new Category("Elektronik");
            elektronik.AddSubCategory(new Category("Telefon"));
            elektronik.AddSubCategory(new Category("Televizyon"));
            elektronik.AddSubCategory(new Category("Araç-Gereç"));

            var giyim = new Category("Giyim");
            giyim.AddSubCategory(new Category("Kadın"));
            giyim.AddSubCategory(new Category("Erkek"));
            giyim.AddSubCategory(new Category("Ayakkabı"));
            giyim.AddSubCategory(new Category("Çanta"));
            giyim.AddSubCategory(new Category("Spor"));

            var bebek = new Category("Bebek");
            bebek.AddSubCategory(new Category("Bakım"));
            bebek.AddSubCategory(new Category("Oyuncak"));

            var bakım = new Category("Bakım");
            bakım.AddSubCategory(new Category("Cilt"));
            bakım.AddSubCategory(new Category("Kozmetik"));

            var ev = new Category("Ev");
            ev.AddSubCategory(new Category("Tadilat"));
            ev.AddSubCategory(new Category("Aksesuar"));
            ev.AddSubCategory(new Category("Yapı"));
            ev.AddSubCategory(new Category("Beyaz Eşya"));

            context.Categories.AddRange(elektronik, giyim, bebek, bakım, ev);
            context.SaveChanges();
        }
    }
}