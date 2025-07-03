using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json;

namespace ikincielkralı.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;
        public string DamageStatus { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public string ListingNumber { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        
        [Obsolete("Kullanılmıyor, çoklu fotoğraf için PhotoPathsJson kullanın.")]
        public string PhotoPath { get; set; } = string.Empty;

        [Display(Name = "Fotoğraflar")]
        public string? PhotoPathsJson { get; set; } // DB'de JSON olarak saklanır

        public List<string> PhotoPaths
        {
            get => string.IsNullOrEmpty(PhotoPathsJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(PhotoPathsJson) ?? new List<string>();
            set => PhotoPathsJson = JsonSerializer.Serialize(value ?? new List<string>());
        }

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
