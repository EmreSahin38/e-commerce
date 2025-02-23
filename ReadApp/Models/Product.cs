#nullable enable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadApp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage = "Ürün adı zorunlu")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sayfa Sayısı adı zorunlu")]
        [Range(0, 50000)]
        [Display(Name = "Sayfa Sayısı")]
        public decimal? Pages { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Categori adı zorunlu")]
        [Display(Name = "Categori")]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}