using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
    [ForeignKey("ParentCategoryId")]
    public Category? ParentCategory { get; set; }
    public List<Category> SubCategories { get; set; }

    public Category(string name)
    {
        Name = name;
        SubCategories = new List<Category>();
    }

    public void AddSubCategory(Category subCategory)
    {
        SubCategories.Add(subCategory);
    }

    public void Display(int depth = 0)
    {
        Console.WriteLine(new string('-', depth) + Name);
        foreach (var subCategory in SubCategories)
        {
            subCategory.Display(depth + 1);
        }
    }
}

public partial class Program
{
    public static void DisplayCategories()
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

        var categories = new List<Category> { elektronik, giyim, bebek, bakım, ev };

        foreach (var category in categories)
        {
            category.Display();
        }
    }
}