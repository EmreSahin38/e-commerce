using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ikincielkralı.Models
{
    public class ListingCreateViewModel
    {
        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string Condition { get; set; } = string.Empty;

        [Required]
        public string DamageStatus { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "En az bir fotoğraf yüklemelisiniz.")]
        [Display(Name = "Fotoğraflar")]
        public List<IFormFile>? Photos { get; set; }

        [Obsolete("Kullanılmıyor, çoklu fotoğraf için Photos kullanın.")]
        [Display(Name = "Fotoğraf")]
        public IFormFile? Photo { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Fiyat pozitif tam sayı olmalı.")]
        public int Price { get; set; }

        // Araba için
        public string? CarBrand { get; set; }
        public string? CarModel { get; set; }
        public int? CarYear { get; set; }
        public int? CarKm { get; set; }

        // Ev için
        public string? HouseCity { get; set; }
        public string? HouseDistrict { get; set; }
    }
}
