#nullable enable

using System.Collections.Generic;
namespace ReadApp.Models{

    public class ProductViewModel{

        public List<Product> Products {get;set;} = null!;
        public string SearchString { get; set; } = null!;
        public string Category { get; set; } = null!;
        public List<Category> Categories { get; set; } = new List<Category>();
        public int? SelectedCategory { get; set; }

    }
}