#nullable enable
using System.ComponentModel.DataAnnotations;

namespace ReadApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "StandartUsers"; // Varsayılan rol "StandartUsers"
    }
}